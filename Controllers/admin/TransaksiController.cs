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
    public class TransaksiController : Controller
    {
        private readonly MvcLaundryContext _context;

        public TransaksiController(MvcLaundryContext context)
        {
            _context = context;
        }

        // GET: Transaksi
        public async Task<IActionResult> Index()
        {
            var mvcLaundryContext = _context.Transaksi.Include(t => t.JenisPakaian).Include(t => t.Users);
            return View(await mvcLaundryContext.ToListAsync());
        }

        // GET: Transaksi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaksi = await _context.Transaksi
                .Include(t => t.JenisPakaian)
                .Include(t => t.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaksi == null)
            {
                return NotFound();
            }

            return View(transaksi);
        }

        // GET: Transaksi/Create
        public IActionResult Create()
        {
            ViewData["JenisPakaianId"] = new SelectList(_context.JenisPakaian, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Transaksi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NamaUser,AlamatUser,NoHPUser,TglTransaksi,TotalTransaksi,JenisPakaianId,UserId")] Transaksi transaksi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaksi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JenisPakaianId"] = new SelectList(_context.JenisPakaian, "Id", "Id", transaksi.JenisPakaianId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", transaksi.UserId);
            return View(transaksi);
        }

        // GET: Transaksi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaksi = await _context.Transaksi.FindAsync(id);
            if (transaksi == null)
            {
                return NotFound();
            }
            ViewData["JenisPakaianId"] = new SelectList(_context.JenisPakaian, "Id", "Id", transaksi.JenisPakaianId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", transaksi.UserId);
            return View(transaksi);
        }

        // POST: Transaksi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaUser,AlamatUser,NoHPUser,TglTransaksi,TotalTransaksi,JenisPakaianId,UserId")] Transaksi transaksi)
        {
            if (id != transaksi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaksi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaksiExists(transaksi.Id))
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
            ViewData["JenisPakaianId"] = new SelectList(_context.JenisPakaian, "Id", "Id", transaksi.JenisPakaianId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", transaksi.UserId);
            return View(transaksi);
        }

        // GET: Transaksi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaksi = await _context.Transaksi
                .Include(t => t.JenisPakaian)
                .Include(t => t.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaksi == null)
            {
                return NotFound();
            }

            return View(transaksi);
        }

        // POST: Transaksi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaksi = await _context.Transaksi.FindAsync(id);
            _context.Transaksi.Remove(transaksi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaksiExists(int id)
        {
            return _context.Transaksi.Any(e => e.Id == id);
        }
    }
}
