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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;


namespace MvcLaundry.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcLaundryContext _context;

        public HomeController(ILogger<HomeController> logger, MvcLaundryContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="User")]  
        public async Task<ActionResult> Users()  
        {  
            return View(await _context.Users.ToListAsync()); 
        } 

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult OrderLaundry(){
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["JenisPakaianId"] = new SelectList(_context.JenisPakaian, "Id", "NamaPakaian");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["UserId1"] = userId.ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderLaundry([Bind("Id,NamaUser,AlamatUser,NoHPUser,TglTransaksi,TotalTransaksi,JenisPakaianId,UserId")] Transaksi transaksi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaksi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JenisPakaianId"] = new SelectList(_context.JenisPakaian, "Id", "Id", transaksi.JenisPakaianId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", transaksi.UserId);
            return View("Home");
        }

        public async Task<IActionResult> RiwayatTransaksi(){
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transaksi = await _context.Transaksi
                .Include(t => t.JenisPakaian)
                .Include(t => t.Users)
                .FirstOrDefaultAsync(m => m.UserId.Equals(userId.ToString()));
            var mvcLaundryContext = _context.Transaksi.Include(t => t.JenisPakaian).Include(t => t.Users);
            return View(await mvcLaundryContext.ToListAsync());
        }

        public IActionResult ContactUs(){
            return View();
        }
    }
}
