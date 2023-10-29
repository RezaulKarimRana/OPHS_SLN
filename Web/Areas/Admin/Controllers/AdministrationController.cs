using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Models.ViewModel;
using static Web.Models.ApplicationConstants;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdministrationController : Controller
    {
        private ApplicationDbContext _context;
        public AdministrationController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> HeadMaster()
        {
            var data = await _context.HeadMaster.FirstOrDefaultAsync();
            if (data == null)
                data = new HeadMaster();
            return View(data);
        }
        public async Task<IActionResult> Chairman()
        {
            var data = await _context.Chairman.FirstOrDefaultAsync();
            if (data == null)
                data = new Chairman();
            return View(data);
        }
        public async Task<IActionResult> GoverningBody()
        {
            var data = await GetCommonData();
            return View(data);
        }
        public async Task<IActionResult> TeacherList()
        {
            var data = await GetCommonData();
            return View(data);
        }
        public async Task<IActionResult> OfficialsList()
        {
            var data = await GetCommonData();
            return View(data);
        }
        public async Task<IActionResult> Employees()
        {
            var data = await _context.Member.Where(x => x.DesignationId == (int)DesignationType.Employee).ToListAsync();
            if(data.Any())
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
            return View(data);
        }
        public async Task<DashBoardVM> GetCommonData()
        {
            var institute = await _context.Institute.FirstOrDefaultAsync();
            var data = new DashBoardVM
            {
                InstituteName = institute == null ? string.Empty : institute.Name,
            };
            return data;
        }

        [HttpPost]
        public async Task<IActionResult> SaveHeadMaster(HeadMaster model)
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
                if(data == null)
                {
                    model.Image = base64Image;
                    _context.HeadMaster.Add(model);
                }
                else
                {
                    data.Name = model.Name;
                    data.Details = model.Details;
                    data.Image = model.ImgFiles == null ? data.Image : base64Image;
                    _context.HeadMaster.Update(data);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Json(null);
        }
        [HttpPost]
        public async Task<IActionResult> SaveChairman(Chairman model)
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
                if(data == null)
                {
                    model.Image = base64Image;
                    _context.Chairman.Add(model);
                }
                else
                {
                    data.Name = model.Name;
                    data.Details = model.Details;
                    data.Image = model.ImgFiles == null ? data.Image : base64Image;
                    _context.Chairman.Update(data);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Json(null);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEmployee(Member model)
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
                return Ok(ex.Message);
            }
            return Json(null);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(Member model)
        {
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
                return Ok(ex.Message);
            }
            return Json(null);
        }
    }
}
