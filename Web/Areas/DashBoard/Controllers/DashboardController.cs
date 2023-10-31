using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models.ViewModel;
using static Web.Models.ApplicationConstants;

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
            var institute = await _context.Institute.FirstOrDefaultAsync();
            var banner = await _context.Banner.ToListAsync();
            var allNotice = await _context.Notice.OrderByDescending(x => x.Id).ToListAsync();
            var data = new DashBoardVM
            {
                InstituteName = institute == null ? string.Empty : institute.Name,
                InstituteAddress = institute == null ? string.Empty : institute.Address,
                InstitutePostalAddress = institute == null ? string.Empty : institute.PostalAddress,
                Notices = allNotice
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
            var headMaster = await _context.Member.Where(x => x.DesignationId == (int)DesignationType.HeadMaster).FirstOrDefaultAsync();
            if (headMaster != null)
            {
                var folderName = string.Empty;
                var imgPrefix = "data:image/jpeg;base64,";
                folderName = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", headMaster.FilePath);
                var memoryStream = new MemoryStream();

                using (var stream = new FileStream(folderName, FileMode.Open))
                {
                    await stream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0;
                headMaster.Base64Image = imgPrefix + Convert.ToBase64String(memoryStream.ToArray());
                data.HeadMasterName = headMaster.Name;
                data.HeadMasterImage = headMaster.Base64Image;
            }
            var chairman = await _context.Member.Where(x => x.DesignationId == (int)DesignationType.Chairman).FirstOrDefaultAsync();
            if (chairman != null)
            {
                var folderName = string.Empty;
                var imgPrefix = "data:image/jpeg;base64,";
                folderName = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", chairman.FilePath);
                var memoryStream = new MemoryStream();

                using (var stream = new FileStream(folderName, FileMode.Open))
                {
                    await stream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0;
                chairman.Base64Image = imgPrefix + Convert.ToBase64String(memoryStream.ToArray());
                data.ChairmanName = chairman.Name;
                data.ChairmanImage = chairman.Base64Image;
            }
            return View(data);
        }
    }
}