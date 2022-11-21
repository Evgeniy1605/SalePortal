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
        private readonly SalePortalDbConnection _context;

        private readonly IHtmlLocalizer<HomeController> _localizer;
        public readonly ICommodityHttpClient _commodityHttpClient;
        public HomeController(SalePortalDbConnection context, IHtmlLocalizer<HomeController> localizer, ICommodityHttpClient commodityHttpClient)
        {
            _commodityHttpClient= commodityHttpClient;
            _context = context;
            _localizer = localizer;
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
            //var result = _context.commodities.Where(x => x.Name.Contains(item.ToLower().Trim()));
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
           var result =  _context.commodities.Where(x => x.TypeId== id);
            return View("Index", await result.ToListAsync());
        }


    }
}