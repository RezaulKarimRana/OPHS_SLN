using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models.ViewModel;

namespace Web.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    public class AdmissionController : Controller
    {
        private ApplicationDbContext _context;
        public AdmissionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> HowToApply()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<IActionResult> Application()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<IActionResult> AdmissionTest()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<IActionResult> AdmissionRules()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<IActionResult> CurrentAdmissionStructure()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }

        public async Task<DashBoardVM> GetDashBoardData()
        {
            var banner = await _context.Banner.ToListAsync();
            var dashBoardModel = new DashBoardVM();
            dashBoardModel.Banner1Src = banner[0].Path;
            dashBoardModel.Banner2Src = banner[1].Path;
            dashBoardModel.Banner3Src = banner[3].Path;
            dashBoardModel.Banner4Src = banner[3].Path;
            dashBoardModel.Banner5Src = banner[4].Path;
            dashBoardModel.Banner6Src = banner[5].Path;
            return dashBoardModel;
        }
    }
}