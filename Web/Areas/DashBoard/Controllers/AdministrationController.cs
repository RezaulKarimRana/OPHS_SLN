﻿using Microsoft.AspNetCore.Mvc;
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
            var headMaster = await _context.HeadMaster.FirstOrDefaultAsync();
            var data = await GetCommonData();
            data.HeadMasterName = headMaster == null ? string.Empty : headMaster.Name;
            data.HeadMasterImage = headMaster == null ? string.Empty : headMaster.Image;
            data.HeadMasterDetails = headMaster == null ? string.Empty : headMaster.Details;
            return View(data);
        }
        public async Task<IActionResult> Chairman()
        {
            var chairman = await _context.Chairman.FirstOrDefaultAsync();
            var data = await GetCommonData();
            data.ChairmanName = chairman == null ? string.Empty : chairman.Name;
            data.ChairmanImage = chairman == null ? string.Empty : chairman.Image;
            data.ChairmanDetails = chairman == null ? string.Empty : chairman.Details;
            return View(data);
        }
        public async Task<IActionResult> GoverningBody()
        {
            var data = await GetCommonData();
            return View(data);
        }
        public async Task<IActionResult> TeacherList()
        {
            var data = await GetCommonData();
            return View(data);
        }
        public async Task<IActionResult> OfficialsList()
        {
            var data = await GetCommonData();
            return View(data);
        }
        public async Task<IActionResult> EmployeeList()
        {
            var data = await GetCommonData();
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