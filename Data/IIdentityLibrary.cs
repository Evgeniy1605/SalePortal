using SalePortal.Models;
using System.Security.Claims;

namespace SalePortal.Data
{
    public interface IIdentityLibrary
    {
        public ValueTask<ClaimsPrincipal> ValidateUserDataAsync(string username, string password);
        public Task ToRegisterAUser(UserInputModel inputModel);
    }
}
