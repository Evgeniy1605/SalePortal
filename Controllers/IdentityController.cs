using AutoMapper;
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
        private readonly IMapper _mapper;

        public IdentityController(SalePortalDbConnection context, ILibrary library, IMapper mapper)
        {
            _mapper= mapper;
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
            password = _library.ToHashPassword(password);
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
            if (ModelState.IsValid)
            {
                userInput.Password = _library.ToHashPassword(userInput.Password);
                UserEntity user = _mapper.Map<UserEntity>(userInput);
                await _library.ToRegisterAUser(user);
                ViewData["Succeeded"] = "Registration succeeded !!!";
                return View("Index");
            }
            return View("Error");
        }
    }
}
