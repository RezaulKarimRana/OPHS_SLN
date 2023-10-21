using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models.ViewModel;

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
            var bannerVM = new BannerVM
            {
                Banner1Src = banner[0].Path,
                Banner2Src = banner[1].Path,
                Banner3Src = banner[3].Path,
                Banner4Src = banner[3].Path,
                Banner5Src = banner[4].Path,
                Banner6Src = banner[5].Path
            };
            return View(bannerVM);
        }
        public async Task<IActionResult> HeadMasterAsync()
        {
            var headMaster = await _context.HeadMaster.FirstOrDefaultAsync();
            var data = new HeadMasterVM
            {
                Id = headMaster.Id,
                Name = headMaster.Name,
                Details = headMaster.Details,
                Image = headMaster.Image
            };
            return View(data);
        }
        public async Task<IActionResult> HeadMasterDetailsAsync()
        {
            var headMaster = await _context.HeadMaster.FirstOrDefaultAsync();
            var data = new HeadMasterVM
            {
                Id = headMaster.Id,
                Name = headMaster.Name,
                Details = headMaster.Details,
                Image = headMaster.Image
            };
            return View(data);
        }
        public async Task<IActionResult> Chairman()
        {
            var chairman = await _context.Chairman.FirstOrDefaultAsync();
            var data = new ChairmanVM
            {
                Id = chairman.Id,
                Name = chairman.Name,
                Details = chairman.Details,
                Image = chairman.Image
            };
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
                return Ok(ex.Message);
            }
            return Json(null);
        }
        [HttpPost]
        public async Task<IActionResult> SaveHeadMaster(HeadMasterVM model)
        {
            try
            {
                string base64Image = "data:image/jpeg;base64,";
                if (model.ImgFiles != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.ImgFiles.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        base64Image += Convert.ToBase64String(fileBytes);
                    }
                }
                var data = await _context.HeadMaster.FirstOrDefaultAsync();
                data.Name = model.Name;
                data.Details = model.Details;
                data.Image = model.ImgFiles == null ? data.Image : base64Image;
                _context.HeadMaster.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Json(null);
        }
        [HttpPost]
        public async Task<IActionResult> SaveChairman(ChairmanVM model)
        {
            try
            {
                string base64Image = "data:image/jpeg;base64,";
                if (model.ImgFiles != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.ImgFiles.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        base64Image += Convert.ToBase64String(fileBytes);
                    }
                }
                var data = await _context.Chairman.FirstOrDefaultAsync();
                data.Name = model.Name;
                data.Details = model.Details;
                data.Image = model.ImgFiles == null ? data.Image : base64Image;
                _context.Chairman.Update(data);
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
