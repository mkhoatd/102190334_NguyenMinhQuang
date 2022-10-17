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
    public class HoaDonController : Controller
    {
        private readonly AppDbContext _context;

        public HoaDonController(AppDbContext context)
        {
            _context = context;
        }

        // GET: HoaDon
        public record SearchInfo(int? Id, string? Name, string? Email, int? LoaiHoaDonId);

        // GET: HoaDon
        public async Task<IActionResult> Index(SearchInfo info)
        {
            // return _context.HoaDons != null ? 
            //             View(await _context.HoaDons.ToListAsync()) :
            //             Problem("Entity set 'AppDbContext.HoaDons'  is null.");
            var res = await _context.HoaDons.Where(h => info.Id == null || h.Id == info.Id)
                .Where(h => info.Name == null || h.Ten.Contains(info.Name))
                .Where(h => info.Email == null || h.EmailKhachHang.Contains(info.Email))
                .Where(h => info.LoaiHoaDonId == -1 || info.LoaiHoaDonId == null || h.LoaiHoaDonId == info.LoaiHoaDonId)
                .ToListAsync();
            var loaiHoaDons = await _context.LoaiHoaDons.ToListAsync();
            var loaiHoaDonsData = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Tất cả",
                    Value = "-1",
                    Selected = true
                }
            };
            foreach (var loaiHoaDon in loaiHoaDons)
            {
                loaiHoaDonsData.Add(new SelectListItem
                {
                    Text=loaiHoaDon.Ten,
                    Value = loaiHoaDon.Id.ToString()
                });
            }

            ViewBag.LoaiHoaDonsData = loaiHoaDonsData;
            return View(res);
        }
        // GET: HoaDon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoaDons == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.LoaiHoaDon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: HoaDon/Create
        public IActionResult Create()
        {
            ViewData["LoaiHoaDonId"] = new SelectList(_context.LoaiHoaDons, "Id", "Id");
            return View();
        }

        // POST: HoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,MaHoaDon,EmailKhachHang,NgayTao,HinhAnh,LoaiHoaDonId")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaiHoaDonId"] = new SelectList(_context.LoaiHoaDons, "Id", "Id", hoaDon.LoaiHoaDonId);
            return View(hoaDon);
        }

        // GET: HoaDon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoaDons == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            ViewData["LoaiHoaDonId"] = new SelectList(_context.LoaiHoaDons, "Id", "Id", hoaDon.LoaiHoaDonId);
            return View(hoaDon);
        }

        // POST: HoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,MaHoaDon,EmailKhachHang,NgayTao,HinhAnh,LoaiHoaDonId")] HoaDon hoaDon)
        {
            if (id != hoaDon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.Id))
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
            ViewData["LoaiHoaDonId"] = new SelectList(_context.LoaiHoaDons, "Id", "Id", hoaDon.LoaiHoaDonId);
            return View(hoaDon);
        }

        // GET: HoaDon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoaDons == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.LoaiHoaDon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoaDons == null)
            {
                return Problem("Entity set 'AppDbContext.HoaDons'  is null.");
            }
            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon != null)
            {
                _context.HoaDons.Remove(hoaDon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(int id)
        {
          return (_context.HoaDons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
