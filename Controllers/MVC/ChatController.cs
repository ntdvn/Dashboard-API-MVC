using Microsoft.AspNetCore.Mvc;

namespace DashboardMVC.Controllers.MVC
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}