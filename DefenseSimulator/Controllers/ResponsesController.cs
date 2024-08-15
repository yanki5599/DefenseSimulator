using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DefenseSimulator.Data;
using DefenseSimulator.Models;
using DefenseSimulator.Services;

namespace DefenseSimulator.Controllers
{
    public class ResponsesController : Controller
    {
        private readonly DefenseSimulatorContext _context;
        private readonly ThreatHandlerService _threatHandlerService;

        public ResponsesController(DefenseSimulatorContext context, ThreatHandlerService threatHandlerService)
        {
            _context = context;
            _threatHandlerService = threatHandlerService;
        }

        // GET: Responses
        public async Task<IActionResult> Index()
        {
            var defenseSimulatorContext = _context.Arsenal.Include(a => a.DefenseWeapon);
            return View(await defenseSimulatorContext.ToListAsync());
        }

       

        private bool ResponseExists(int id)
        {
            return _context.Response.Any(e => e.ResponseId == id);
        }
    }
}
