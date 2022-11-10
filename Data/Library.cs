using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using SalePortal.DbConnection;
using System.Security.Claims;
using SalePortal;
using Microsoft.EntityFrameworkCore;

namespace SalePortal.Data;

internal  class Library : ILibrary
{
    private readonly SalePortalDbConnection _context;
    public Library(SalePortalDbConnection context)
    {
        _context = context;
    }
    public  int GetUserId(List<Claim> claims)
    {
        return int.Parse(claims.ToList()[0].ToString().Split(':')[2].Trim());
    }

    public  ClaimsPrincipal ValidateUserData(string username, string password)
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
            
            return ClaimsPrincipal;
        }
        else
        {
            var ClaimsPrincipal = new ClaimsPrincipal();
            return ClaimsPrincipal;
        }
        
    }
}
