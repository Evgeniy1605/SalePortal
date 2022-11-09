using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Security.Claims;

namespace SalePortal.Data;

internal  class Library : ILibrary
{
  
    
    public  int GetUserId(List<Claim> claims)
    {
        return int.Parse(claims.ToList()[0].ToString().Split(':')[2].Trim());
    }
}
