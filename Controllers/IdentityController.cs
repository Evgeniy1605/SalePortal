using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalePortal.Data;
using SalePortal.DbConnection;
using SalePortal.Models;
using System.Security.Claims;

namespace SalePortal.Controllers
{
    public class IdentityController : Controller
    {

        private readonly SalePortalDbConnection _context;
        private readonly ILibrary _library;

        public IdentityController(SalePortalDbConnection context, ILibrary library)
        {
            _library = library;
            _context = context;
        }


        [HttpGet("login")]
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ValidateData(string username, string password)
        {
            var ClaimsPrincipal = _library.ValidateUserData(username, password);
            if (ClaimsPrincipal.Identity != null)
            {
                await HttpContext.SignInAsync(ClaimsPrincipal);
                return RedirectToAction("Index", "Home", new {aria =""});
            }

            ViewData["Message"] = "Wrong username or password";
            return View("Index");

        }

        [Authorize]
        public async Task<IActionResult> UserPage()
        {
            int userId = _library.GetUserId(User.Claims.ToList());
            var ads = _context.commodities.Where(x => x.OwnerId == userId);
            return View(await ads.ToListAsync());
        }

        public IActionResult Registration()
        {
            UserInputModel userInputModel = new UserInputModel();
            return View(userInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserInputModel userInput)
        {
            return View("Index");
        }
    }
}
