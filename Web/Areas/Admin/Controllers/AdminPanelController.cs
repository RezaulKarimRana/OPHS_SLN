using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminPanelController : Controller
    {
        public AdminPanelController()
        {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
