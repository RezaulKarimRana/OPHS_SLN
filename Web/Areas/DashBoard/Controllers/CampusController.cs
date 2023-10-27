using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models.ViewModel;

namespace Web.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    public class CampusController : Controller
    {
        private ApplicationDbContext _context;
        public CampusController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AboutOurs()
        {
            var aboutOurs = await _context.AboutOurs.FirstOrDefaultAsync();
            var data = await GetCommonData();
            data.AboutOurself = aboutOurs == null ? string.Empty : aboutOurs.AboutOurself;
            return View(data);
        }
        public async Task<IActionResult> History()
        {
            var aboutOurs = await _context.AboutOurs.FirstOrDefaultAsync();
            var data = await GetCommonData();
            data.History = aboutOurs == null ? string.Empty : aboutOurs.History;
            return View(data);
        }
        public async Task<IActionResult> Aims()
        {
            var aboutOurs = await _context.AboutOurs.FirstOrDefaultAsync();
            var data = await GetCommonData();
            data.Aims = aboutOurs == null ? string.Empty : aboutOurs.Aims;
            return View(data);
        }
        public async Task<IActionResult> InstitutionalStructure()
        {
            var aboutOurs = await _context.AboutOurs.FirstOrDefaultAsync();
            var data = await GetCommonData();
            data.InstitutionalStructure = aboutOurs == null ? string.Empty : aboutOurs.InstitutionalStructure;
            return View(data);
        }
        public async Task<DashBoardVM> GetCommonData()
        {
            var institute = await _context.Institute.FirstOrDefaultAsync();
            var banner = await _context.Banner.ToListAsync();
            var data = new DashBoardVM
            {
                InstituteName = institute == null ? string.Empty : institute.Name
            };
            if (banner != null)
            {
                if (banner.Count >= 1)
                    data.Banner1Src = banner[0].Path;
                if (banner.Count >= 2)
                    data.Banner2Src = banner[1].Path;
                if (banner.Count >= 3)
                    data.Banner3Src = banner[2].Path;
                if (banner.Count >= 4)
                    data.Banner4Src = banner[3].Path;
                if (banner.Count >= 5)
                    data.Banner5Src = banner[4].Path;
                if (banner.Count >= 6)
                    data.Banner6Src = banner[5].Path;
            }
            return data;
        }
    }
}