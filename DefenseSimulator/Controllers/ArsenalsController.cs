using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DefenseSimulator.Data;
using DefenseSimulator.Models;

namespace DefenseSimulator.Controllers
{
    public class ArsenalsController : Controller
    {
        private readonly DefenseSimulatorContext _context;

        public ArsenalsController(DefenseSimulatorContext context)
        {
            _context = context;
        }

        // GET: Arsenals
        public async Task<IActionResult> Index()
        {
            var defenseSimulatorContext = _context.Arsenal.Include(a => a.DefenseWeapon);
            return View(await defenseSimulatorContext.ToListAsync());
        }

        // GET: Arsenals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arsenal = await _context.Arsenal
                .Include(a => a.DefenseWeapon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arsenal == null)
            {
                return NotFound();
            }

            return View(arsenal);
        }

        // GET: Arsenals/Create
        public IActionResult Create()
        {
            ViewData["DefenseWeaponId"] = new SelectList(_context.Set<DefenseWeapon>(), "Id", "Type");
            return View();
        }

        // POST: Arsenals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,DefenseWeaponId")] Arsenal arsenal)
        {
            if (ModelState.IsValid)
            {
                if(await _context.Arsenal.FirstOrDefaultAsync(ars=>ars.DefenseWeaponId == arsenal.DefenseWeaponId) != null)
                {
                    ViewData["errors"] = "weapon type already exist";
                    ViewData["DefenseWeaponId"] = new SelectList(_context.Set<DefenseWeapon>(), "Id", "Type");

                    return View(arsenal);
                }
                _context.Add(arsenal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DefenseWeaponId"] = new SelectList(_context.Set<DefenseWeapon>(), "Id", "Type", arsenal.DefenseWeaponId);
            return View(arsenal);
        }

        // GET: Arsenals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arsenal = await _context.Arsenal.FindAsync(id);
            if (arsenal == null)
            {
                return NotFound();
            }
            ViewData["DefenseWeaponId"] = new SelectList(_context.Set<DefenseWeapon>(), "Id", "Type", arsenal.DefenseWeaponId);
            return View(arsenal);
        }

        // POST: Arsenals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,DefenseWeaponId")] Arsenal arsenal)
        {
            if (id != arsenal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arsenal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArsenalExists(arsenal.Id))
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
            ViewData["DefenseWeaponId"] = new SelectList(_context.Set<DefenseWeapon>(), "Id", "Type", arsenal.DefenseWeaponId);
            return View(arsenal);
        }

        // GET: Arsenals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arsenal = await _context.Arsenal
                .Include(a => a.DefenseWeapon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arsenal == null)
            {
                return NotFound();
            }

            return View(arsenal);
        }

        // POST: Arsenals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var arsenal = await _context.Arsenal.FindAsync(id);
            if (arsenal != null)
            {
                _context.Arsenal.Remove(arsenal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArsenalExists(int id)
        {
            return _context.Arsenal.Any(e => e.Id == id);
        }
    }
}
