using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using SalePortal.Data;
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

        public async Task< IActionResult> Index()
        {
            var text = _localizer["Hello"];
            ViewData["Text"] = text;
            var comodities = await _commodityHttpClient.GetCommoditiesAsync();
            
            return View(comodities.OrderByDescending(x => x.PublicationDate).ToList());
        }

        public async Task<IActionResult> Search(string item)
        {
            var comodities = await _commodityHttpClient.GetCommoditiesAsync();
            var result = comodities.Where(x => x.Name.Contains(item.ToLower().Trim()));
            return View("Index", result.ToList());
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

        public async Task<IActionResult> FilterCategory(int id)
        {
            var commotities = await _commodityHttpClient.GetCommoditiesAsync();
            var result = commotities.Where(x => x.TypeId == id).ToList();
            return View("Index",  result);
        }

        [Authorize]
        public async Task<IActionResult> AddOrderForBuyingCommodity(int commodityId)
        {
            var userId = _library.GetUserId(User.Claims.ToList());
            await _orderCommodity.AddOrderAsync(commodityId, userId);
            return View();
        }
    }
}