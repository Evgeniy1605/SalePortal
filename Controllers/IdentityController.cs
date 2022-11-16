using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using SalePortal.Data;
using SalePortal.Models;
using System.Security.Claims;


namespace SalePortal.Controllers
{
    public class IdentityController : Controller
    {

        private readonly SalePortalDbConnection _context;
        private readonly ILibrary _library;
        private readonly IHtmlLocalizer<IdentityController> _localizer;
        private readonly IIdentityLibrary _identityLibrary;

        public IdentityController(SalePortalDbConnection context, ILibrary library, IHtmlLocalizer<IdentityController> localizer, IIdentityLibrary identityLibrary)
        {
            _localizer = localizer;
            _library = library;
            _context = context;
            _identityLibrary = identityLibrary;
        }


        [HttpGet("login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidateData(string username, string password)
        {
            var ClaimsPrincipal = _identityLibrary.ValidateUserData(username, password);
            if (ClaimsPrincipal.Identity != null)
            {
                await HttpContext.SignInAsync(ClaimsPrincipal);
                return RedirectToAction("Index", "Home", new {aria =""});
            }
            var message = _localizer["Wrong username or password!!!"];
            ViewData["Message"] = message;
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
            if (ModelState.IsValid)
            {
                await _identityLibrary.ToRegisterAUser(userInput);
                var succeededMessage = _localizer["Registration succeeded !!!"];
                ViewData["Succeeded"] = succeededMessage;
                return View("Index");
            }
            return View("Error");
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home", new { aria = "" });
        }

        [HttpGet("denied")]
        public IActionResult DeniedPage() { return View(); }
    }
}
