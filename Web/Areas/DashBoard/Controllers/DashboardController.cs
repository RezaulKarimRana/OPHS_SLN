using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web.Data;
using Web.Models;
using Web.Models.ViewModel;

namespace Web.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    public class DashboardController : Controller
    {
        private ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dashBoardModel = await GetCommonData();
            return View(dashBoardModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<DashBoardVM> GetCommonData()
        {
            var institute = await _context.Institute.FirstOrDefaultAsync();
            var banner = await _context.Banner.ToListAsync();
            var headMaster = await _context.HeadMaster.FirstOrDefaultAsync();
            var chairman = await _context.Chairman.FirstOrDefaultAsync();
            var allNotice = await _context.Notice.OrderByDescending(x => x.Id).ToListAsync();
            var noticeVM = new List<NoticeVM>();
            if(allNotice != null)
            {
                foreach (var item in allNotice)
                {
                    noticeVM.Add(new NoticeVM
                    {
                        Id = item.Id,
                        Serial = ConvertEnToBn(item.Id.ToString()),
                        Name = item.Name,
                        CreatedDate = ConvertEnToBn(item.CreatedDate)
                    });
                }
            }
            var dashBoardModel = new DashBoardVM
            {
                InstituteName = institute == null ? string.Empty : institute.Name,
                InstituteAddress = institute == null ? string.Empty : institute.Address,
                InstitutePostalAddress = institute == null ? string.Empty : institute.PostalAddress,
                HeadMasterName = headMaster == null ? string.Empty : headMaster.Name,
                HeadMasterImage = headMaster == null ? string.Empty : headMaster.Image,
                ChairmanName =  chairman == null ? string.Empty : chairman.Name,
                ChairmanImage =  chairman == null ? string.Empty : chairman.Image,
                Notices = noticeVM
            };
            if(banner != null)
            {
                if (banner.Count >= 1)
                    dashBoardModel.Banner1Src = banner[0].Path;
                if (banner.Count >= 2)
                    dashBoardModel.Banner2Src = banner[1].Path;
                if (banner.Count >= 3)
                    dashBoardModel.Banner3Src = banner[2].Path;
                if (banner.Count >= 4)
                    dashBoardModel.Banner4Src = banner[3].Path;
                if (banner.Count >= 5)
                    dashBoardModel.Banner5Src = banner[4].Path;
                if (banner.Count >= 6)
                    dashBoardModel.Banner6Src = banner[5].Path;
            }
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
    }
}