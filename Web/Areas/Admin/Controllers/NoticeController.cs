using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Models.ViewModel;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NoticeController : Controller
    {
        private ApplicationDbContext _context;
        public NoticeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult NoticeCreate()
        {
            return View();
        }
        public async Task<IActionResult> NoticeList()
        {
            var data = await _context.Notice.OrderByDescending(x => x.Id).ToListAsync();
            var response = new List<NoticeVM>();
            foreach (var item in data)
            {
                response.Add(new NoticeVM
                {
                    Id = item.Id,
                    Serial = ConvertEnToBn(item.Id.ToString()),
                    Name = item.Name,
                    CreatedDate = ConvertEnToBn(item.CreatedDate),
                    FileName = item.FileName
                });
            }
            return View(response);
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
        public string ConvertEnToBn(string data)
        {
            return data.Replace("0", "\u09E6")
                    .Replace("1", "\u09E7")
                    .Replace("2", "\u09E8")
                    .Replace("3", "\u09E9")
                    .Replace("4", "\u09EA")
                    .Replace("5", "\u09EB")
                    .Replace("6", "\u09EC")
                    .Replace("7", "\u09ED")
                    .Replace("8", "\u09EE")
                    .Replace("9", "\u09EF");
        }
    }
}
