using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcLaundry.Data;
using MvcLaundry.Models;

namespace MvcLaundry.Controllers.admin
{
    public class PegawaiController : Controller
    {
        private readonly MvcLaundryContext _context;

        public PegawaiController(MvcLaundryContext context)
        {
            _context = context;
        }

        // GET: Pegawai
        public async Task<IActionResult> Index()
        {
            var pegawai = from m in _context.Users
                 select m;
            pegawai = pegawai.Where(s => s.Role.Contains("Admin"));
            return View(await pegawai.ToListAsync());
        }

        // GET: Pegawai/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pegawai = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pegawai == null)
            {
                return NotFound();
            }

            return View(pegawai);
        }

        // GET: Pegawai/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pegawai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Name,EmailId,Password,Role,DateOfBirth,Address")] Users pegawai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pegawai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pegawai);
        }

        // GET: Pegawai/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pegawai = await _context.Users.FindAsync(id);
            if (pegawai == null)
            {
                return NotFound();
            }
            return View(pegawai);
        }

        // POST: Pegawai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Name,EmailId,Password,Role,DateOfBirth,Address")] Users pegawai)
        {
            if (id != pegawai.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pegawai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PegawaiExists(pegawai.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pegawai);
        }

        // GET: Pegawai/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pegawai = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pegawai == null)
            {
                return NotFound();
            }

            return View(pegawai);
        }

        // POST: Pegawai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pegawai = await _context.Users.FindAsync(id);
            _context.Users.Remove(pegawai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PegawaiExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
