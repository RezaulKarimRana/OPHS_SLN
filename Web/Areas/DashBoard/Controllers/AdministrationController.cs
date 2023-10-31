using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models.ViewModel;
using static Web.Models.ApplicationConstants;

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
            var headMaster = await _context.Member.Where(x=> x.DesignationId == (int)DesignationType.HeadMaster).FirstOrDefaultAsync();
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
            }
            var speech = await _context.Speech.FirstOrDefaultAsync();
            var data = await GetCommonData();
            data.HeadMasterName = headMaster == null ? string.Empty : headMaster.Name;
            data.HeadMasterImage = headMaster == null ? string.Empty : headMaster.Base64Image;
            data.HeadMasterSpeech = speech == null ? string.Empty : speech.HeadMasterSpeech;
            return View(data);
        }
        public async Task<IActionResult> Chairman()
        {
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
            }
            var speech = await _context.Speech.FirstOrDefaultAsync();
            var data = await GetCommonData();
            data.ChairmanName = chairman == null ? string.Empty : chairman.Name;
            data.ChairmanImage = chairman == null ? string.Empty : chairman.Base64Image;
            data.ChairmanSpeech = speech == null ? string.Empty : speech.ChairmanSpeech;
            return View(data);
        }
        public async Task<IActionResult> GoverningBody()
        {
            var folderName = string.Empty;
            var imgPrefix = "data:image/jpeg;base64,";
            var data = await GetCommonData();
            data.GoverningBody = await _context.Member.Where(x => (x.DesignationId == (int)DesignationType.Chairman) || (x.DesignationId == (int)DesignationType.Assistant_Chairman) || (x.DesignationId == (int)DesignationType.Committe_Member)).OrderBy(p=> p.DesignationId).ToListAsync();
            if (data.GoverningBody != null)
            {
                foreach(var item in data.GoverningBody)
                {
                    folderName = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", item.FilePath);
                    var memoryStream = new MemoryStream();

                    using (var stream = new FileStream(folderName, FileMode.Open))
                    {
                        await stream.CopyToAsync(memoryStream);
                    }
                    memoryStream.Position = 0;
                    item.Base64Image = imgPrefix + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return View(data);
        }
        public async Task<IActionResult> TeacherList()
        {
            var folderName = string.Empty;
            var imgPrefix = "data:image/jpeg;base64,";
            var data = await GetCommonData();
            data.Teacher = await _context.Member.Where(x => (x.DesignationId == (int)DesignationType.HeadMaster) || (x.DesignationId == (int)DesignationType.Assistant_Teacher) || (x.DesignationId == (int)DesignationType.Teacher)).OrderBy(p => p.DesignationId).ToListAsync();
            if (data.Teacher != null)
            {
                foreach (var item in data.Teacher)
                {
                    folderName = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", item.FilePath);
                    var memoryStream = new MemoryStream();

                    using (var stream = new FileStream(folderName, FileMode.Open))
                    {
                        await stream.CopyToAsync(memoryStream);
                    }
                    memoryStream.Position = 0;
                    item.Base64Image = imgPrefix + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return View(data);
        }
        public async Task<IActionResult> OfficialsList()
        {
            var folderName = string.Empty;
            var imgPrefix = "data:image/jpeg;base64,";
            var data = await GetCommonData();
            data.Officials = await _context.Member.Where(x => x.DesignationId == (int)DesignationType.Officials).OrderBy(p => p.DesignationId).ToListAsync();
            if (data.Officials != null)
            {
                foreach (var item in data.Officials)
                {
                    folderName = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", item.FilePath);
                    var memoryStream = new MemoryStream();

                    using (var stream = new FileStream(folderName, FileMode.Open))
                    {
                        await stream.CopyToAsync(memoryStream);
                    }
                    memoryStream.Position = 0;
                    item.Base64Image = imgPrefix + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return View(data);
        }
        public async Task<IActionResult> EmployeeList()
        {
            var folderName = string.Empty;
            var imgPrefix = "data:image/jpeg;base64,";
            var data = await GetCommonData();
            data.Employees = await _context.Member.Where(x => x.DesignationId == (int)DesignationType.Employee).OrderBy(p => p.DesignationId).ToListAsync();
            if (data.Employees != null)
            {
                foreach (var item in data.Employees)
                {
                    folderName = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", item.FilePath);
                    var memoryStream = new MemoryStream();

                    using (var stream = new FileStream(folderName, FileMode.Open))
                    {
                        await stream.CopyToAsync(memoryStream);
                    }
                    memoryStream.Position = 0;
                    item.Base64Image = imgPrefix + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
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