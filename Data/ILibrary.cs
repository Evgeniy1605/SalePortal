using System.Security.Claims;

namespace SalePortal.Data
{
    public interface ILibrary
    {
        public int GetUserId(List<Claim> claims);
    }
}
