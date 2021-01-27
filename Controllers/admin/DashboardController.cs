using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcLaundry.Models;
using Microsoft.AspNetCore.Authorization; 
using MvcLaundry.Data; 
using Microsoft.EntityFrameworkCore;

namespace MvcLaundry.Controllers.admin
{
    public class DashboardController : Controller
    {
        private readonly MvcLaundryContext _context;

        public DashboardController(MvcLaundryContext context)
        {
            _context = context;
        }

        [Authorize(Roles ="Admin")] 
        public IActionResult Index()
        {
            return View();
        }
    }
}
