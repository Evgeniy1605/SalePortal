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

 
}
