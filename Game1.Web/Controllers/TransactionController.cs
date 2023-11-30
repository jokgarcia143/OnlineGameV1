using Microsoft.AspNetCore.Mvc;

namespace Game1.Web.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
