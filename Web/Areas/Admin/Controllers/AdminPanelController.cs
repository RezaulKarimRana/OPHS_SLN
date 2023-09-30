using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> UploadBanner(IFormFile banner, int id)
        {
            if(banner == null)
            {
                return Ok();
            }
            var path = Path.Combine(_environment.WebRootPath + "\\img\\banner", DateTime.Now.Millisecond.ToString() + "_" +Path.GetFileName(banner.FileName));
            try
            {
                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    await banner.CopyToAsync(fileStream);
                }
                var data = await _context.Banner.Where(x => x.Id == id).FirstOrDefaultAsync();
                data.Path = path;
                _context.Banner.Update(data);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return Ok();
            }
            return Json(null);
        }
    }
}
