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
        private readonly ILogger<DashboardController> _logger;
        private ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context, ILogger<DashboardController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dashBoardModel = await GetDashBoardData();
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
    }
}