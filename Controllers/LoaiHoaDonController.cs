using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _102190334_NguyenMinhQuang.Models;

namespace _102190334_NguyenMinhQuang.Controllers
{
    public class LoaiHoaDonController : Controller
    {
        private readonly AppDbContext _context;

        public LoaiHoaDonController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LoaiHoaDon
        public async Task<IActionResult> Index()
        {
              return _context.LoaiHoaDons != null ? 
                          View(await _context.LoaiHoaDons.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.LoaiHoaDons'  is null.");
        }

        // GET: LoaiHoaDon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoaiHoaDons == null)
            {
                return NotFound();
            }

            var loaiHoaDon = await _context.LoaiHoaDons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaiHoaDon == null)
            {
                return NotFound();
            }

            return View(loaiHoaDon);
        }

        // GET: LoaiHoaDon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiHoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ten")] LoaiHoaDon loaiHoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiHoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiHoaDon);
        }

        // GET: LoaiHoaDon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoaiHoaDons == null)
            {
                return NotFound();
            }

            var loaiHoaDon = await _context.LoaiHoaDons.FindAsync(id);
            if (loaiHoaDon == null)
            {
                return NotFound();
            }
            return View(loaiHoaDon);
        }

        // POST: LoaiHoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten")] LoaiHoaDon loaiHoaDon)
        {
            if (id != loaiHoaDon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiHoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiHoaDonExists(loaiHoaDon.Id))
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
            return View(loaiHoaDon);
        }

        // GET: LoaiHoaDon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoaiHoaDons == null)
            {
                return NotFound();
            }

            var loaiHoaDon = await _context.LoaiHoaDons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaiHoaDon == null)
            {
                return NotFound();
            }

            return View(loaiHoaDon);
        }

        // POST: LoaiHoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoaiHoaDons == null)
            {
                return Problem("Entity set 'AppDbContext.LoaiHoaDons'  is null.");
            }
            var loaiHoaDon = await _context.LoaiHoaDons.FindAsync(id);
            if (loaiHoaDon != null)
            {
                _context.LoaiHoaDons.Remove(loaiHoaDon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiHoaDonExists(int id)
        {
          return (_context.LoaiHoaDons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
