using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BBKBootCamp.Data;
using BBKBootCamp.Servicios;
using BBKBootCamp.Models;

namespace BBKBootCamp.Controllers
{
    public class HoraDisponiblesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HoraDisponiblesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HoraDisponibles
        public async Task<IActionResult> Index()
        {
            return View(await _context.HoraDisponible.ToListAsync());
        }

        // GET: HoraDisponibles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horaDisponible = await _context.HoraDisponible
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horaDisponible == null)
            {
                return NotFound();
            }

            return View(horaDisponible);
        }

        // GET: HoraDisponibles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HoraDisponibles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaHora,Estado")] HoraDisponible horaDisponible)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horaDisponible);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(horaDisponible);
        }

        // GET: HoraDisponibles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horaDisponible = await _context.HoraDisponible.FindAsync(id);
            if (horaDisponible == null)
            {
                return NotFound();
            }
            return View(horaDisponible);
        }

        // POST: HoraDisponibles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaHora,Estado")] HoraDisponible horaDisponible)
        {
            if (id != horaDisponible.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horaDisponible);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoraDisponibleExists(horaDisponible.Id))
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
            return View(horaDisponible);
        }

        // GET: HoraDisponibles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horaDisponible = await _context.HoraDisponible
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horaDisponible == null)
            {
                return NotFound();
            }

            return View(horaDisponible);
        }

        // POST: HoraDisponibles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horaDisponible = await _context.HoraDisponible.FindAsync(id);
            _context.HoraDisponible.Remove(horaDisponible);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoraDisponibleExists(int id)
        {
            return _context.HoraDisponible.Any(e => e.Id == id);
        }
    }
}
