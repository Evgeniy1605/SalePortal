using Microsoft.AspNetCore.Mvc;

namespace SalePortal.Controllers
{
    public class ChatController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
