using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CampusController : Controller
    {
        private ApplicationDbContext _context;
        public CampusController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> AboutOurs()
        {
            var data = await _context.AboutOurs.FirstOrDefaultAsync();
            if (data == null)
                data = new AboutOurs();
            return View(data);
        }
        public async Task<IActionResult> History()
        {
            var data = await _context.AboutOurs.FirstOrDefaultAsync();
            if (data == null)
                data = new AboutOurs();
            return View(data);
        }
        public async Task<IActionResult> Aims()
        {
            var data = await _context.AboutOurs.FirstOrDefaultAsync();
            if (data == null)
                data = new AboutOurs();
            return View(data);
        }
        public async Task<IActionResult> InstitutionalStructure()
        {
            var data = await _context.AboutOurs.FirstOrDefaultAsync();
            if (data == null)
                data = new AboutOurs();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> SaveAboutOurs(AboutOurs model)
        {
            try
            {
                var data = await _context.AboutOurs.FirstOrDefaultAsync();
                if (data == null)
                {
                    model.Aims = string.Empty;
                    model.History = string.Empty;
                    model.InstitutionalStructure = string.Empty;
                    _context.AboutOurs.Add(model);
                }
                else
                {
                    data.AboutOurself = model.AboutOurself;
                    _context.AboutOurs.Update(data);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
            return Json(new { Success = true });
        }
        [HttpPost]
        public async Task<IActionResult> SaveHistory(AboutOurs model)
        {
            try
            {
                var data = await _context.AboutOurs.FirstOrDefaultAsync();
                if (data == null)
                {
                    model.Aims = string.Empty;
                    model.AboutOurself = string.Empty;
                    model.InstitutionalStructure = string.Empty;
                    _context.AboutOurs.Add(model);
                }
                else
                {
                    data.History = model.History;
                    _context.AboutOurs.Update(data);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
            return Json(new { Success = true });
        }
        [HttpPost]
        public async Task<IActionResult> SaveAims(AboutOurs model)
        {
            try
            {
                model.AboutOurself = string.Empty;
                model.History = string.Empty;
                model.InstitutionalStructure = string.Empty;
                var data = await _context.AboutOurs.FirstOrDefaultAsync();
                if (data == null)
                {
                    _context.AboutOurs.Add(model);
                }
                else
                {
                    data.Aims = model.Aims;
                    _context.AboutOurs.Update(data);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
            return Json(new { Success = true });
        }
        [HttpPost]
        public async Task<IActionResult> SaveInstitutionalStructure(AboutOurs model)
        {
            try
            {
                var data = await _context.AboutOurs.FirstOrDefaultAsync();
                if (data == null)
                {
                    model.Aims = string.Empty;
                    model.History = string.Empty;
                    model.AboutOurself = string.Empty;
                    _context.AboutOurs.Add(model);
                }
                else
                {
                    data.InstitutionalStructure = model.InstitutionalStructure;
                    _context.AboutOurs.Update(data);
                }
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
