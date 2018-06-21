using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScoringSystem.Data;
using ScoringSystem.Models;

namespace ScoringSystem.Controllers
{
    public class PowerFactorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PowerFactorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PowerFactors
        public async Task<IActionResult> Index()
        {
            return View(await _context.PowerFactor.ToListAsync());
        }

        // GET: PowerFactors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powerFactor = await _context.PowerFactor
                .SingleOrDefaultAsync(m => m.PowerID == id);
            if (powerFactor == null)
            {
                return NotFound();
            }

            return View(powerFactor);
        }

        // GET: PowerFactors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PowerFactors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PowerID,PowerName")] PowerFactor powerFactor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(powerFactor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(powerFactor);
        }

        // GET: PowerFactors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powerFactor = await _context.PowerFactor.SingleOrDefaultAsync(m => m.PowerID == id);
            if (powerFactor == null)
            {
                return NotFound();
            }
            return View(powerFactor);
        }

        // POST: PowerFactors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PowerID,PowerName")] PowerFactor powerFactor)
        {
            if (id != powerFactor.PowerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(powerFactor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PowerFactorExists(powerFactor.PowerID))
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
            return View(powerFactor);
        }

        // GET: PowerFactors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powerFactor = await _context.PowerFactor
                .SingleOrDefaultAsync(m => m.PowerID == id);
            if (powerFactor == null)
            {
                return NotFound();
            }

            return View(powerFactor);
        }

        // POST: PowerFactors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var powerFactor = await _context.PowerFactor.SingleOrDefaultAsync(m => m.PowerID == id);
            _context.PowerFactor.Remove(powerFactor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PowerFactorExists(int id)
        {
            return _context.PowerFactor.Any(e => e.PowerID == id);
        }
    }
}
