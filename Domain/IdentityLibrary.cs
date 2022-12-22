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

    public   ClaimsPrincipal ValidateUserData(string username, string password)
    {
        password = ToHashPassword(password);
        //
        var admins =  _admins.GetAdmins();
        var users =  _userHttp.GetUsers();
        var expectedUser = users.SingleOrDefault(x => x.Name == username && x.Password == password);
        var expectedAdmin = admins.SingleOrDefault(x => x.Name == username && x.Password == password);

        if (expectedUser != null)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, expectedUser.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, expectedUser.Name));
            claims.Add(new Claim("Password", password));
            claims.Add(new Claim("SurName", expectedUser.SurName));
            claims.Add(new Claim("Email", expectedUser.EmailAddress));
            claims.Add(new Claim("PhoneNumber", expectedUser.PhoneNumber));
            claims.Add(new Claim(ClaimTypes.Role, "User"));
            var ClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var ClaimsPrincipal = new ClaimsPrincipal(ClaimsIdentity);

            return ClaimsPrincipal;
        }
        else if (expectedAdmin != null)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, expectedAdmin.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, expectedAdmin.Name));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
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
