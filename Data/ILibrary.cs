using System.Security.Claims;

namespace SalePortal.Data
{
    public interface ILibrary
    {
        public int GetUserId(List<Claim> claims);

        public ClaimsPrincipal ValidateUserData(string username, string password);
    }
}
