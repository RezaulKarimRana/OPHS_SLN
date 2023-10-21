using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models.ViewModel;

namespace Web.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    public class AdministrationController : Controller
    {
        private ApplicationDbContext _context;
        public AdministrationController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> HeadMaster()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<IActionResult> Chairman()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<IActionResult> GoverningBody()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<IActionResult> AcademicCouncil()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<IActionResult> TeacherList()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<IActionResult> OfficialsList()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<IActionResult> EmployeeList()
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
                HeadMasterDetails = headMaster.Details,
                HeadMasterImage = headMaster.Image,
                ChairmanName = chairman.Name,
                ChairmanDetails = chairman.Details,
                ChairmanImage = chairman.Image
            };
            return dashBoardModel;
        }
    }
}