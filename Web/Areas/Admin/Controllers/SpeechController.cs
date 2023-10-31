using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpeechController : Controller
    {
        private ApplicationDbContext _context;
        public SpeechController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> HeadMasterSpeech()
        {
            var data = await _context.Speech.FirstOrDefaultAsync();
            if (data == null)
                data = new Speech();
            return View(data);
        }
        public async Task<IActionResult> ChairmanSpeech()
        {
            var data = await _context.Speech.FirstOrDefaultAsync();
            if (data == null)
                data = new Speech();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> SaveHeadMasterSpeech(Speech model)
        {
            try
            {
                var data = await _context.Speech.FirstOrDefaultAsync();
                if (data == null)
                {
                    model.ChairmanSpeech = string.Empty;
                    _context.Speech.Add(model);
                }
                else
                {
                    data.HeadMasterSpeech = model.HeadMasterSpeech;
                    _context.Speech.Update(data);
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
        public async Task<IActionResult> SaveChairmanSpeech(Speech model)
        {
            try
            {
                var data = await _context.Speech.FirstOrDefaultAsync();
                if (data == null)
                {
                    model.HeadMasterSpeech = string.Empty;
                    _context.Speech.Add(model);
                }
                else
                {
                    data.ChairmanSpeech = model.ChairmanSpeech;
                    _context.Speech.Update(data);
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
