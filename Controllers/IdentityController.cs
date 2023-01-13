using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using SalePortal.Data;
using SalePortal.Models;
using System.Security.Claims;


namespace SalePortal.Controllers
{
    public class IdentityController : Controller
    {

        private readonly IOrderCommodity _orderCommodity;
        private readonly IUserHttpClient _userHttp;
        private readonly IEmailSender _emailSender;
        private readonly ILibrary _library;
        private readonly IHtmlLocalizer<IdentityController> _localizer;
        private readonly IIdentityLibrary _identityLibrary;
        private readonly ICommodityHttpClient _commodityHttpClient;
        private readonly IPasswordRecovery _passwordRecovery;
        public IdentityController(ILibrary library, IHtmlLocalizer<IdentityController> localizer, IIdentityLibrary identityLibrary, 
            ICommodityHttpClient commodityHttpClient, 
            IOrderCommodity orderCommodity,
            IUserHttpClient userHttp,
            IEmailSender emailSender,
            IPasswordRecovery passwordRecovery)
        {
            _localizer = localizer;
            _library = library;
            _identityLibrary = identityLibrary;
            _commodityHttpClient = commodityHttpClient;
            _orderCommodity= orderCommodity;
            _userHttp = userHttp;
            _emailSender = emailSender;
            _passwordRecovery = passwordRecovery;
        }


        [HttpGet("login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidateData(string username, string password)
        {
            var ClaimsPrincipal = await _identityLibrary.ValidateUserDataAsync(username, password);
            if (ClaimsPrincipal.Identity != null)
            {
                await HttpContext.SignInAsync(ClaimsPrincipal);
                return RedirectToAction("Index", "Home", new {aria =""});
            }
            var message = _localizer["Wrong username or password!!!"];
            ViewData["Message"] = message;
            return View("Index");
        }

        [Authorize]
        public async Task<IActionResult> UserPage()
        {
            int userId = _library.GetUserId(User.Claims.ToList());
            var ads = await _commodityHttpClient.GetCommoditiesAsync();
            return View(ads.Where(x => x.OwnerId == userId).ToList());
        }

        public IActionResult Registration()
        {
            UserInputModel userInputModel = new UserInputModel();
            return View(userInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserInputModel userInput)
        {
            if (ModelState.IsValid)
            {
                await _identityLibrary.ToRegisterAUser(userInput);
                var succeededMessage = _localizer["Registration succeeded !!!"];
                ViewData["Succeeded"] = succeededMessage;
                return View("Index");
            }
            return View("Error");
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home", new { aria = "" });
        }

        [HttpGet("denied")]
        public IActionResult DeniedPage() { return View(); }


        [Authorize(Roles = "Admin")]
        public IActionResult AdminPage()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AddOrderForBuyingCommodity(int commodityId)
        {
            var userId = _library.GetUserId(User.Claims.ToList());
            await _orderCommodity.AddOrderAsync(commodityId, userId);
            return RedirectToAction("UserPage", "Identity", new { aria = "" });
        }

        

        [Authorize]
        public async Task<IActionResult> DetailsOfOrder(int id)
        {
            var order = await _orderCommodity.GetOrderAsync(id);
            return View(order);
        }

        [Authorize]
        public async Task<IActionResult> ApproveOrder(int orderId)
        {
            var order = await _orderCommodity.GetOrderAsync(orderId);
            var userId = _library.GetUserId(User.Claims.ToList());

            if (userId != order.CommodityOwnerId)
            {
                return View("Error");
            }

            if (order.ApprovedByOwner == false)
            {
                await _orderCommodity.ApproveOrderAsync(orderId);
                return PartialView("_ApprovedOrder");
            }
            if (order.ApprovedByOwner == true)
            {
                await _orderCommodity.UnApproveOrderAsync(orderId);
                return PartialView("_UnApprovedOrder");
            }
            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderCommodity.GetOrderAsync(id);
            int userId = _library.GetUserId(User.Claims.ToList());

            if (order == null || userId != order.CustomerId && userId != order.CommodityOwnerId)
            {
                return View("Error");
            }
            await _orderCommodity.RemoveOrderAsync(id);
            return RedirectToAction("UserPage", "Identity", new {aria=""});
        }

        public IActionResult password()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordRecovery(UserInputModel userInput)
        {
            var users =  _userHttp.GetUsers();
            var user = users.SingleOrDefault(x => x.Name == userInput.Name && x.EmailAddress == userInput.EmailAddress );
            if (user == null)
            {
                ViewData["NotFound"] = "User with this data was not found";
                return View("Index");
            }
            Random random = new Random();
            var code = random.Next(1000, 9999);
            await _emailSender.SendAsync(user.EmailAddress, $"CODE: {code}");

            PasswordRecoveryModel recoveryModel = new PasswordRecoveryModel()
            {
                Code = code,
                UserId = user.Id,
            };
            return View(recoveryModel);
        }

        [HttpPost]
         public  IActionResult ValidateCode(PasswordRecoveryModel recoveryModel)
         {
            if (recoveryModel.Code != recoveryModel.InputCode)
            {
                ViewData["WrongCode"] = "Wrong Code";
                return View("Index");
            }
            
            return View("ChangePassword", recoveryModel);
         }

        public async Task<IActionResult> NewPassword(PasswordRecoveryModel recoveryModel)
        {
            await _passwordRecovery.ChangePasswordAsync(recoveryModel.UserId, recoveryModel.NewPassword);
            ViewData["PasswordChanged"] = "The password was changed successfully!!!";

            return View("Index");
        }

    }
}
