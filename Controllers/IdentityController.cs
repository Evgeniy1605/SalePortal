using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalePortal.DbConnection;
using SalePortal.Models;
using System.Security.Claims;

namespace SalePortal.Controllers
{
    public class IdentityController : Controller
    {

        private readonly SalePortalDbConnection _context;

        public IdentityController(SalePortalDbConnection context)
        {
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
            var expectedUser = _context.Users.SingleOrDefault(x => x.Name == username || x.Password == password);
            
            if (expectedUser != null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, expectedUser.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, expectedUser.Name));
                claims.Add(new Claim("Password", password));
                claims.Add(new Claim("SurName", expectedUser.SurName));
                claims.Add(new Claim("Email", expectedUser.EmailAddress));
                claims.Add(new Claim("PhoneNumber", expectedUser.PhoneNumber));
                var ClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var ClaimsPrincipal = new ClaimsPrincipal(ClaimsIdentity);
                await HttpContext.SignInAsync(ClaimsPrincipal);
                return RedirectToAction("Index", "Home", new {aria =""});
            }

            ViewData["Message"] = "Wrong username or password";
            return View("Index");

        }

        [Authorize]
        public async Task<IActionResult> UserPage()
        {
            var claimId = User.Claims.First();
            var userId = int.Parse(claimId.ToString().Split(':')[2].Trim()) ;
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
