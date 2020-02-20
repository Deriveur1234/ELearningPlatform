using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELearningPlatform.Data;
using ELearningPlatform.Models;

namespace ELearningPlatform.Controllers
{
    public class CompletedCoursesController : Controller
    {
        private readonly ELearningPlatformContext _context;

        public CompletedCoursesController(ELearningPlatformContext context)
        {
            _context = context;
        }

        // GET: CompletedCourses
        public async Task<IActionResult> Index()
        {
            return View(await _context.CompletedCourse.ToListAsync());
        }

        // GET: CompletedCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var completedCourse = await _context.CompletedCourse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (completedCourse == null)
            {
                return NotFound();
            }

            return View(completedCourse);
        }

        // GET: CompletedCourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompletedCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdParticipation,IdPage,IsCompleted")] CompletedCourse completedCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(completedCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(completedCourse);
        }

        // GET: CompletedCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var completedCourse = await _context.CompletedCourse.FindAsync(id);
            if (completedCourse == null)
            {
                return NotFound();
            }
            return View(completedCourse);
        }

        // POST: CompletedCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdParticipation,IdPage,IsCompleted")] CompletedCourse completedCourse)
        {
            if (id != completedCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(completedCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompletedCourseExists(completedCourse.Id))
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
            return View(completedCourse);
        }

        // GET: CompletedCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var completedCourse = await _context.CompletedCourse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (completedCourse == null)
            {
                return NotFound();
            }

            return View(completedCourse);
        }

        // POST: CompletedCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var completedCourse = await _context.CompletedCourse.FindAsync(id);
            _context.CompletedCourse.Remove(completedCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompletedCourseExists(int id)
        {
            return _context.CompletedCourse.Any(e => e.Id == id);
        }
    }
}
