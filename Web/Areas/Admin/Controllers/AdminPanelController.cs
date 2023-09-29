using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Banner()
        {
            return View();
        }
        public IActionResult MegaBanner()
        {
            return View();
        }
    }
}
