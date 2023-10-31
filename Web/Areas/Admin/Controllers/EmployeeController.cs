using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using static Web.Models.ApplicationConstants;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Member.Where(x => x.DesignationId == (int)DesignationType.Employee).ToListAsync();
            if (data != null)
            {
                var folderName = string.Empty;
                var imgPrefix = "data:image/jpeg;base64,";
                foreach (var item in data)
                {
                    folderName = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", item.FilePath);
                    var memoryStream = new MemoryStream();

                    using (var stream = new FileStream(folderName, FileMode.Open))
                    {
                        await stream.CopyToAsync(memoryStream);
                    }
                    memoryStream.Position = 0;
                    item.Base64Image = imgPrefix + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return new JsonResult(data);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Member model)
        {
            if (model.ImgFiles == null)
            {
                return Ok("Please add an Image");
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
                var member = new Member
                {
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    DesignationId = (int)DesignationType.Employee,
                    FileName = model.ImgFiles.FileName,
                    FileType = model.ImgFiles.ContentType,
                    FilePath = fileName,
                    CreatedDate = DateTime.Now.ToShortDateString()
                };
                _context.Member.Add(member);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
            return Json(new { Success = true });
        }
        [HttpPost]
        public async Task<IActionResult> Update(Member model)
        {
            try
            {
                var data = await _context.Member.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                if(data == null)
                    return BadRequest();
                var fileName = string.Empty;
                data.Name = model.Name;
                data.PhoneNumber = model.PhoneNumber;
                data.DesignationId = (int)DesignationType.Employee;
                data.CreatedDate = DateTime.Now.ToShortDateString();
                if (model.ImgFiles != null)
                {
                    var previousInfo = await _context.Member.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if(previousInfo != null && previousInfo.FilePath != null)
                    {
                        var pathToDelete = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", previousInfo.FilePath);
                        var file_delete = new FileInfo(pathToDelete);
                        if (file_delete.Exists)
                        {
                            file_delete.Delete();
                        }
                    }
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                    fileName = Guid.NewGuid().ToString() + "_" + model.ImgFiles.FileName;
                    Directory.CreateDirectory(pathToSave);
                    string filePath = Path.Combine(pathToSave, fileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImgFiles.CopyToAsync(fileStream);
                    }
                    data.FileName = model.ImgFiles.FileName;
                    data.FileType = model.ImgFiles.ContentType;
                    data.FilePath = fileName;
                }
                _context.Member.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
            return Json(new { Success = true });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var previousInfo = await _context.Member.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (previousInfo != null && previousInfo.FilePath != null)
                {
                    var pathToDelete = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", previousInfo.FilePath);
                    var file_delete = new FileInfo(pathToDelete);
                    if (file_delete.Exists)
                    {
                        file_delete.Delete();
                    }
                    _context.Member.Remove(previousInfo);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
            return Json(new { Success = true });
        }
    }
}
