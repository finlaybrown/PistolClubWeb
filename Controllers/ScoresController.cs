using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScoringSystem.Data;
using ScoringSystem.Models;

namespace ScoringSystem.Controllers
{
    public class ScoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Scores
        public async Task<IActionResult> Index(string sortOrder, string searchString, DateTime dateString, string MemberString)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["dateFilter"] = dateString;
            ViewData["memberFilter"] = MemberString;

            if (!String.IsNullOrEmpty(searchString))
            {
                var applicationDbContext = _context.Score.Include(s => s.Division).Include(s => s.Grade).Include(s => s.Power).Include(s => s.User).Where(s => s.StageName.Contains(searchString));
                return View(await applicationDbContext.ToListAsync());
            }
            else if (dateString != DateTime.MinValue)
            {
                var applicationDbContext = _context.Score.Include(s => s.Division).Include(s => s.Grade).Include(s => s.Power).Include(s => s.User).Where(s => s.ShootDate.Date.Equals(dateString));
                return View(await applicationDbContext.ToListAsync());
            }
            else if (!String.IsNullOrEmpty(MemberString))
            {
                var applicationDbContext = _context.Score.Include(s => s.Division).Include(s => s.Grade).Include(s => s.Power).Include(s => s.User).Where(s => s.User.UserFirstName.Contains(MemberString));// || s => s.User.UserLastName.Contains(MemberString));
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Score.Include(s => s.Division).Include(s => s.Grade).Include(s => s.Power).Include(s => s.User);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Scores/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .Include(s => s.Division)
                .Include(s => s.Grade)
                .Include(s => s.Power)
                .Include(s => s.User)
                .SingleOrDefaultAsync(m => m.ScoreID == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // GET: Scores/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["DivisionID"] = new SelectList(_context.Division, "DivisionID", "DivisionName");
            ViewData["GradeID"] = new SelectList(_context.Grade, "GradeID", "GradeName");
            ViewData["PowerID"] = new SelectList(_context.PowerFactor, "PowerID", "PowerName");
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "Email");
            return View();
        }

        // POST: Scores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ScoreID,UserID,GradeID,PowerID,DivisionID,StageName,Points,Penalty,Time,HitFactor,ShootDate,StagePoints")] Score score)
        {
            if (ModelState.IsValid)
            {
                _context.Add(score);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivisionID"] = new SelectList(_context.Division, "DivisionID", "DivisionName", score.DivisionID);
            ViewData["GradeID"] = new SelectList(_context.Grade, "GradeID", "GradeName", score.GradeID);
            ViewData["PowerID"] = new SelectList(_context.PowerFactor, "PowerID", "PowerName", score.PowerID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "Email", score.UserID);
            return View(score);
        }

        // GET: Scores/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score.SingleOrDefaultAsync(m => m.ScoreID == id);
            if (score == null)
            {
                return NotFound();
            }
            ViewData["DivisionID"] = new SelectList(_context.Division, "DivisionID", "DivisionName", score.DivisionID);
            ViewData["GradeID"] = new SelectList(_context.Grade, "GradeID", "GradeName", score.GradeID);
            ViewData["PowerID"] = new SelectList(_context.PowerFactor, "PowerID", "PowerName", score.PowerID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "Email", score.UserID);
            return View(score);
        }

        // POST: Scores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ScoreID,UserID,GradeID,PowerID,DivisionID,StageName,Points,Penalty,Time,HitFactor,ShootDate,StagePoints")] Score score)
        {
            if (id != score.ScoreID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(score);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreExists(score.ScoreID))
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
            ViewData["DivisionID"] = new SelectList(_context.Division, "DivisionID", "DivisionName", score.DivisionID);
            ViewData["GradeID"] = new SelectList(_context.Grade, "GradeID", "GradeName", score.GradeID);
            ViewData["PowerID"] = new SelectList(_context.PowerFactor, "PowerID", "PowerName", score.PowerID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "Email", score.UserID);
            return View(score);
        }

        // GET: Scores/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .Include(s => s.Division)
                .Include(s => s.Grade)
                .Include(s => s.Power)
                .Include(s => s.User)
                .SingleOrDefaultAsync(m => m.ScoreID == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var score = await _context.Score.SingleOrDefaultAsync(m => m.ScoreID == id);
            _context.Score.Remove(score);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScoreExists(int id)
        {
            return _context.Score.Any(e => e.ScoreID == id);
        }
    }
}
