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
    public class ThreatsController : Controller
    {
        private readonly DefenseSimulatorContext _context;

        public ThreatsController(DefenseSimulatorContext context)
        {
            _context = context;
        }

        // GET: Threats
        public async Task<IActionResult> Index()
        {
            var defenseSimulatorContext = _context.Threat.Include(t => t.AttackWeapon).Include(t => t.OriginThreat);
            return View(await defenseSimulatorContext.ToListAsync());
        }
       


        // GET: Threats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threat = await _context.Threat
                .Include(t => t.AttackWeapon)
                .Include(t => t.OriginThreat)
                .FirstOrDefaultAsync(m => m.ThreatId == id);
            if (threat == null)
            {
                return NotFound();
            }

            return View(threat);
        }

        // GET: Threats/Create
        public IActionResult Create()
        {
            ViewData["AttackWeaponId"] = new SelectList(_context.AttackWeapon, "Id", "Type");
            ViewData["OriginThreatId"] = new SelectList(_context.OriginThreat, "Id", "Name");
            return View();
        }

        // POST: Threats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThreatId,OriginThreatId,IsActive,Amount,AttackWeaponId")] Threat threat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(threat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AttackWeaponId"] = new SelectList(_context.AttackWeapon, "Id", "Type", threat.AttackWeaponId);
            ViewData["OriginThreatId"] = new SelectList(_context.OriginThreat, "Id", "Name", threat.OriginThreatId);
            return View(threat);
        }

        

        

        // GET: Threats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threat = await _context.Threat
                .Include(t => t.AttackWeapon)
                .Include(t => t.OriginThreat)
                .FirstOrDefaultAsync(m => m.ThreatId == id);
            if (threat == null)
            {
                return NotFound();
            }

            return View(threat);
        }

        // POST: Threats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var threat = await _context.Threat.FindAsync(id);
            if (threat != null)
            {
                _context.Threat.Remove(threat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Post:  Threats/Launch/5
        [HttpPost,ActionName("Launch")]
        public async Task<IActionResult> Launch(int id)
        {
            if(id != null)
            {
                var threat = await _context.Threat.FindAsync(id);
                if(threat != null)
                {
                    threat.LaunchTime = DateTime.Now;
                    threat.IsActive = true;
                    _context.Threat.Update(threat);
                    _context.SaveChanges();
                }
            }
            /*return RedirectToAction(nameof(Index));*/
            return Json(new { success = true, message = "Operation successful" });
        }

        private bool ThreatExists(int id)
        {
            return _context.Threat.Any(e => e.ThreatId == id);
        }
    }
}
