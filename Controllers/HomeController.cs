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
        public HomeController(SalePortalDbConnection context, IHtmlLocalizer<HomeController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task< IActionResult> Index()
        {
            var text = _localizer["Hello"];
            ViewData["Text"] = text;

            return View(await _context.commodities.OrderByDescending(x => x.PublicationDate).ToListAsync());
        }

        public async Task<IActionResult> Search(string item)
        {
            var result = _context.commodities.Where(x => x.Name.Contains(item.ToLower().Trim()));
            return View("Index", await result.ToListAsync());
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