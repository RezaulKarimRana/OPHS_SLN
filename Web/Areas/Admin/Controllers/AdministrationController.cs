using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Models.ViewModel;

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
        public async Task<IActionResult> EmployeeList()
        {
            var data = await GetCommonData();
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
    }
}
