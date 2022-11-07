using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalePortal.DbConnection;
using SalePortal.Models;
using System.Diagnostics;

namespace SalePortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly SalePortalDbConnection _context;
        public HomeController(SalePortalDbConnection context)
        {
            _context = context;
        }

        public async Task< IActionResult> Index()
        {

            return View(await _context.commodities.ToListAsync());
        }

        public async Task<IActionResult> Search(string item)
        {
            var result = _context.commodities.Where(x => x.Name.Contains(item.ToLower().Trim()));
            return View("Index", await result.ToListAsync());
        }




    }
}