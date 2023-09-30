using Microsoft.AspNetCore.Mvc;
using Web.Data;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminPanelController : Controller
    {
        private IWebHostEnvironment _environment;
        private ApplicationDbContext _context;
        public AdminPanelController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
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
        [HttpPost]
        public async Task<IActionResult> UploadBanner(IFormFile banner)
        {
            if(banner == null)
            {
                return Ok();
            }
            var name = Path.Combine(_environment.WebRootPath + "\\img\\banner", DateTime.Now.Millisecond.ToString() + "_" +Path.GetFileName(banner.FileName));
            try
            {
                using (Stream fileStream = new FileStream(name, FileMode.Create))
                {
                    await banner.CopyToAsync(fileStream);
                }
            }
            catch(Exception ex)
            {
                return Ok();
            }
            return Json(null);
        }
    }
}
