using SalePortal.Models;
using System.Security.Claims;

namespace SalePortal.Data
{
    public interface IIdentityLibrary
    {
        public ClaimsPrincipal ValidateUserData(string username, string password);
        public Task ToRegisterAUser(UserInputModel inputModel);
    }
}
