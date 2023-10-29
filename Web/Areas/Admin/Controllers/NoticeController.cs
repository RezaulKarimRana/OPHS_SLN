using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

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
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Notice.OrderByDescending(x=> x.Id).ToListAsync();
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Notice model)
        {
            try
            {
                var serial = 1;
                var allNotice = await _context.Notice.ToListAsync();
                if(allNotice.Any())
                {
                    serial = allNotice.Count + 1;
                }
                var notice = new Notice
                {
                    Serial = ConvertEnToBn(serial.ToString()),
                    Name = model.Name,
                    Details = model.Details,
                    CreatedDate = ConvertEnToBn(DateTime.Now.ToShortDateString())
                };
                _context.Notice.Add(notice);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Json(new { Success = true });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var previousInfo = await _context.Notice.Where(x => x.Id == id).FirstOrDefaultAsync();
                if(previousInfo == null)
                {
                    return Ok();
                }
                _context.Notice.Remove(previousInfo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Json(new { Success = true });
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
