using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models.ViewModel;

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
            var data = await GetData();
            return View(data);
        }
        public async Task<IActionResult> History()
        {
            var data = await GetData();
            return View(data);
        }
        public async Task<IActionResult> Aims()
        {
            var data = await GetData();
            return View(data);
        }
        public async Task<IActionResult> InstitutionalStructure()
        {
            var data = await GetData();
            return View(data);
        }
        public async Task<AboutOursVM> GetData()
        {
            var response = await _context.AboutOurs.FirstOrDefaultAsync();
            var data = new AboutOursVM
            {
                AboutOurself = response.AboutOurself,
                History = response.History,
                Aims = response.Aims,
                InstitutionalStructure = response.InstitutionalStructure
            };
            return data;
        }
        [HttpPost]
        public async Task<IActionResult> SaveAboutOurs(AboutOursVM model)
        {
            try
            {
                var data = await _context.AboutOurs.FirstOrDefaultAsync();
                data.AboutOurself = model.AboutOurself;
                _context.AboutOurs.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Json(null);
        }
        [HttpPost]
        public async Task<IActionResult> SaveHistory(AboutOursVM model)
        {
            try
            {
                var data = await _context.AboutOurs.FirstOrDefaultAsync();
                data.History = model.History;
                _context.AboutOurs.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Json(null);
        }
        [HttpPost]
        public async Task<IActionResult> SaveAims(AboutOursVM model)
        {
            try
            {
                var data = await _context.AboutOurs.FirstOrDefaultAsync();
                data.Aims = model.Aims;
                _context.AboutOurs.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Json(null);
        }
        [HttpPost]
        public async Task<IActionResult> SaveInstitutionalStructure(AboutOursVM model)
        {
            try
            {
                var data = await _context.AboutOurs.FirstOrDefaultAsync();
                data.InstitutionalStructure = model.InstitutionalStructure;
                _context.AboutOurs.Update(data);
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
