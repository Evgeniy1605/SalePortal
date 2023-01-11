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
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace SalePortal.Domain;

public class IdentityLibrary : IIdentityLibrary, IPasswordRecovery
{
    private readonly IMapper _mapper;
    private readonly IUserHttpClient _userHttp;
    private readonly IAdmins _admins;
    public IdentityLibrary(IMapper mapper, IUserHttpClient userHttp, IAdmins admins)
    {
        _mapper = mapper;
        _userHttp = userHttp;
        _admins = admins;
    }

    public async ValueTask< ClaimsPrincipal> ValidateUserDataAsync(string username, string password)
    {
        var user = await _userHttp.ValidateUserDataAsync(username, password);
        if (user.Id == 0)  
            return new ClaimsPrincipal();

        if (user.Role == "Admin")
        {
            var claimsForAdmin = new List<Claim>();
            claimsForAdmin.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claimsForAdmin.Add(new Claim(ClaimTypes.Name, user.Name));
            claimsForAdmin.Add(new Claim(ClaimTypes.Role, user.Role));
            claimsForAdmin.Add(new Claim("Token", user.Token));
            var ClaimsIdentityForAdmin = new ClaimsIdentity(claimsForAdmin, CookieAuthenticationDefaults.AuthenticationScheme);
            var ClaimsPrincipalForAdmin = new ClaimsPrincipal(ClaimsIdentityForAdmin);

            return ClaimsPrincipalForAdmin;
        }
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.Name));
        claims.Add(new Claim("Password", password));
        claims.Add(new Claim("SurName", user.SurName));
        claims.Add(new Claim("Email", user.EmailAddress));
        claims.Add(new Claim("PhoneNumber", user.PhoneNumber));
        claims.Add(new Claim(ClaimTypes.Role, user.Role));
        claims.Add(new Claim("Token", user.Token));
        var ClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var ClaimsPrincipal = new ClaimsPrincipal(ClaimsIdentity);

        return ClaimsPrincipal;


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
        await _userHttp.PostUserAsync(user);
        
    }

    public async Task ChangePasswordAsync(int userId, string newPassword)
    {
        var password = ToHashPassword(newPassword);
        var user = await _userHttp.GetUserByIdAsync(userId);
        user.Password = password;
        await _userHttp.PutUserAsync(userId, user);
    }
}
