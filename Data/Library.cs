using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Security.Claims;

namespace SalePortal.Data;

public static class Library
{
  
    
    public static int GetUserId(List<Claim> claims)
    {
        return int.Parse(claims.ToList()[0].ToString().Split(':')[2].Trim());
    }
}
