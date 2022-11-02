using Microsoft.AspNetCore.Mvc;
using SalePortal.Models;
using System.Diagnostics;

namespace SalePortal.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

      
      
    }
}