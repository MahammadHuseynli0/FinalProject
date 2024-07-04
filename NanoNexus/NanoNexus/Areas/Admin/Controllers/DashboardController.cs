﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NanoNexus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
