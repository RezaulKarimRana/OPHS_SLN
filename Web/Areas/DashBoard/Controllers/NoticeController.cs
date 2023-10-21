﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
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
    }
}