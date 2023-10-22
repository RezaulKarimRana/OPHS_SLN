using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Web.Data;
using Web.Models.ViewModel;

namespace Web.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    public class NoticeController : Controller
    {
        private ApplicationDbContext _context;
        public NoticeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AllNotice()
        {
            var data = await GetDashBoardData();
            var allNotice = await _context.Notice.ToListAsync();
            var response = new List<NoticeVM>();
            foreach (var item in allNotice)
            {
                response.Add(new NoticeVM
                {
                    Id = item.Id,
                    Serial = ConvertEnToBn(item.Id.ToString()),
                    Name = item.Name,
                    CreatedDate = ConvertEnToBn(DateTime.Now.ToShortDateString()),
                    Image = item.Image
                });
            }
            data.Notices = response;
            return View(data);
        }
        public async Task<IActionResult> AllAssignment()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<DashBoardVM> GetDashBoardData()
        {
            var institute = await _context.Institute.FirstOrDefaultAsync();
            var banner = await _context.Banner.ToListAsync();
            var headMaster = await _context.HeadMaster.FirstOrDefaultAsync();
            var chairman = await _context.Chairman.FirstOrDefaultAsync();
            var dashBoardModel = new DashBoardVM
            {
                InstituteName = institute.Name,
                Banner1Src = banner[0].Path,
                Banner2Src = banner[1].Path,
                Banner3Src = banner[3].Path,
                Banner4Src = banner[3].Path,
                Banner5Src = banner[4].Path,
                Banner6Src = banner[5].Path,
                HeadMasterName = headMaster.Name,
                HeadMasterImage = headMaster.Image,
                ChairmanName = chairman.Name,
                ChairmanImage = chairman.Image
            };
            return dashBoardModel;
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
        public async Task<IActionResult> NoticeView(int id)
        {
            var notice = await _context.Notice.Where(x => x.Id == id).FirstOrDefaultAsync();
            var data = await GetDashBoardData();
            data.Notice = new NoticeVM
            {
                Id = notice.Id,
                Name = notice.Name,
                Image = notice.Image
            };
            return View(data);
        }
    }
}