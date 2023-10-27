using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Models.ViewModel;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminPanelController : Controller
    {
        private ApplicationDbContext _context;
        public AdminPanelController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Banner()
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
        public async Task<IActionResult> Notice()
        {
            var allNotice = await _context.Notice.ToListAsync();
            var response = new List<NoticeVM>();
            foreach(var item in allNotice)
            {
                response.Add(new NoticeVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    FileName = item.FileName
                });
            }
            return View(response);
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
        public async Task<IActionResult> SaveNotice(NoticeVM model)
        {
            if(model.ImgFiles == null)
            {
                return Ok("Please add a Notice");
            }
            try
            {
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                var fileName = Guid.NewGuid().ToString() + "_" + model.ImgFiles.FileName;
                if (model.ImgFiles != null)
                {
                    Directory.CreateDirectory(pathToSave);
                    string filePath = Path.Combine(pathToSave, fileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImgFiles.CopyToAsync(fileStream);
                    }
                }
                var notice = new Notice
                {
                    Name = model.Name,
                    FileName = model.ImgFiles.FileName,
                    FileType = model.ImgFiles.ContentType,
                    FilePath = fileName,
                    CreatedDate = DateTime.Now.ToShortDateString()
                };
                _context.Notice.Add(notice);
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
