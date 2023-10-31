using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models.ViewModel;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private ApplicationDbContext _context;
        public BannerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> BannerList()
        {
            var data = new BannerVM();
            var banner = await _context.Banner.ToListAsync();
            if(banner != null)
            {
                if (banner.Count() >= 1)
                    data.Banner1Src = banner[0].Path;
                if (banner.Count() >= 2)
                    data.Banner2Src = banner[1].Path;
                if (banner.Count() >= 3)
                    data.Banner3Src = banner[2].Path;
                if (banner.Count() >= 4)
                    data.Banner4Src = banner[3].Path;
                if (banner.Count() >= 5)
                    data.Banner5Src = banner[4].Path;
                if (banner.Count() >= 6)
                    data.Banner6Src = banner[5].Path;
            }
            return View(data);
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
                return Json(new { Success = false, Message = ex.Message });
            }
            return Json(new { Success = true });
        }
    }
}
