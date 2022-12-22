using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using SalePortal.Data;
using SalePortal.Domain;
using SalePortal.Entities;
using SalePortal.Models;
using System.Diagnostics;

namespace SalePortal.Controllers
{
    public class HomeController : Controller
    {


        private readonly IHtmlLocalizer<HomeController> _localizer;
        private readonly ICommodityHttpClient _commodityHttpClient;
        private readonly IOrderCommodity _orderCommodity;
        private readonly ILibrary _library;
        public HomeController(IHtmlLocalizer<HomeController> localizer, ICommodityHttpClient commodityHttpClient, IOrderCommodity orderCommodity, ILibrary library)
        {
            _commodityHttpClient= commodityHttpClient;
            _localizer = localizer;
            _orderCommodity= orderCommodity;
            _library= library;
        }

        public async Task< IActionResult> Index(int? pageNumber)
        {
            var text = _localizer["Hello"];
            ViewData["Text"] = text;
            var comodities = await _commodityHttpClient.GetCommoditiesAsync();

            int pageSize = 5;
            return View(PagingClass<CommodityEntity>.Create(comodities.OrderByDescending(x => x.PublicationDate).ToList(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Search(string item, int? pageNumber)
        {
            ViewData["item"] = item;
            var comodities = await _commodityHttpClient.GetCommoditiesAsync();
            var result = comodities.Where(x => x.Name.Contains(item.ToLower().Trim()));
            int pageSize = 5;
            return View(PagingClass<CommodityEntity>.Create(result.ToList(), pageNumber ?? 1, pageSize));
        }


        [HttpPost]
        public IActionResult CultureManagment(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(30)
                });
            return LocalRedirect(returnUrl);

        }

        public async Task<IActionResult> FilterCategory(int id, int? pageNumber)
        {
            var commotities = await _commodityHttpClient.GetCommoditiesAsync();
            var result = commotities.Where(x => x.TypeId == id).ToList();
            ViewData["id"] = id;
            int pageSize = 5;
            return View(PagingClass<CommodityEntity>.Create(result.ToList(), pageNumber ?? 1, pageSize));
        }

        
    }
}