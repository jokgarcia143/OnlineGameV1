using Microsoft.AspNetCore.Mvc;

namespace Game1.Web.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
