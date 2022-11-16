using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using AutoMapper;
using SalePortal.Models;
using NuGet.LibraryModel;
using SalePortal.Data;
using SalePortal.Entities;

namespace SalePortal.Domain;

internal class Library : ILibrary
{
    private readonly SalePortalDbConnection _context;
    private readonly IMapper _mapper;
    public Library(SalePortalDbConnection context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    public int GetUserId(List<Claim> claims)
    {
        return int.Parse(claims.ToList()[0].ToString().Split(':')[2].Trim());
    }

    public ClaimsPrincipal ValidateUserData(string username, string password)
    {
        password = ToHashPassword(password);
        var expectedUser = _context.Users.SingleOrDefault(x => x.Name == username && x.Password == password);

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
    private string ToHashPassword(string password)
    {
        var sha = SHA256.Create();
        var asBiteArray = Encoding.Default.GetBytes(password);
        var hash = sha.ComputeHash(asBiteArray);
        return Convert.ToBase64String(hash);
    }
    public async Task ToRegisterAUser(UserInputModel inputUser)
    {
        inputUser.Password = ToHashPassword(inputUser.Password);
        UserEntity user = _mapper.Map<UserEntity>(inputUser);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
