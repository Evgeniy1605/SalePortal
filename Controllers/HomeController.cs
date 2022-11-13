using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using SalePortal.DbConnection;
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




    }
}