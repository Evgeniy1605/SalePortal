using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SalePortal.DbConnection;
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



        //[HttpPost("login")]
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
    }
}
