﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    public class OfficeController : Controller
    {
        private ApplicationDbContext _context;
        public OfficeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Office()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<IActionResult> Employee()
        {
            var dashBoardModel = await GetDashBoardData();
            return View(dashBoardModel);
        }
        public async Task<IActionResult> Officials()
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