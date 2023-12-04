using Microsoft.AspNetCore.Mvc;

namespace Game1.Web.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Deposit()
        {
            return View();
        }
        public IActionResult Withdraw()
        {
            return View();
        }
        public IActionResult SixBox()
        {
            return View();
        }
        public IActionResult TwoBox()
        {
            return View();
        }
    }
}
