using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

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
        public async Task<IActionResult> Banner()
        {
            var banner = await _context.Banner.ToListAsync();
            var dashBoardModel = new BannerVM();
            dashBoardModel.Banner1Src = banner[0].Path;
            dashBoardModel.Banner2Src = banner[1].Path;
            dashBoardModel.Banner3Src = banner[3].Path;
            dashBoardModel.Banner4Src = banner[3].Path;
            dashBoardModel.Banner5Src = banner[4].Path;
            dashBoardModel.Banner6Src = banner[5].Path;
            return View(dashBoardModel);
        }
        public IActionResult HeadMaster()
        {
            return View();
        }
        public IActionResult Chairman()
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
            try
            {
                string base64Image = "data:image/jpeg;base64,";
                using (var ms = new MemoryStream())
                {
                    banner.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    base64Image += Convert.ToBase64String(fileBytes);
                }
                var data = await _context.Banner.Where(x => x.Id == id).FirstOrDefaultAsync();
                data.Path = base64Image;
                _context.Banner.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Json(null);
        }
    }
}
