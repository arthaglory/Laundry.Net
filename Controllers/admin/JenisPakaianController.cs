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
    public class JenisPakaianController : Controller
    {
        private readonly MvcLaundryContext _context;

        public JenisPakaianController(MvcLaundryContext context)
        {
            _context = context;
        }

        // GET: JenisPakaian
        public async Task<IActionResult> Index()
        {
            return View(await _context.JenisPakaian.ToListAsync());
        }

        // GET: JenisPakaian/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jenisPakaian = await _context.JenisPakaian
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jenisPakaian == null)
            {
                return NotFound();
            }

            return View(jenisPakaian);
        }

        // GET: JenisPakaian/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JenisPakaian/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NamaPakaian,HargaPerKg")] JenisPakaian jenisPakaian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jenisPakaian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jenisPakaian);
        }

        // GET: JenisPakaian/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jenisPakaian = await _context.JenisPakaian.FindAsync(id);
            if (jenisPakaian == null)
            {
                return NotFound();
            }
            return View(jenisPakaian);
        }

        // POST: JenisPakaian/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaPakaian,HargaPerKg")] JenisPakaian jenisPakaian)
        {
            if (id != jenisPakaian.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jenisPakaian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JenisPakaianExists(jenisPakaian.Id))
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
            return View(jenisPakaian);
        }

        // GET: JenisPakaian/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jenisPakaian = await _context.JenisPakaian
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jenisPakaian == null)
            {
                return NotFound();
            }

            return View(jenisPakaian);
        }

        // POST: JenisPakaian/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jenisPakaian = await _context.JenisPakaian.FindAsync(id);
            _context.JenisPakaian.Remove(jenisPakaian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JenisPakaianExists(int id)
        {
            return _context.JenisPakaian.Any(e => e.Id == id);
        }
    }
}
