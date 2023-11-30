using Microsoft.AspNetCore.Mvc;

namespace Game1.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        
        public IActionResult AdminView(string userName)
        {
            return View();
        }

        public IActionResult PlayerView(string userName)
        {
            return View();
        }
    }
}
