using Microsoft.AspNetCore.Mvc;

namespace DashboardMVC.Controllers.MVC
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}