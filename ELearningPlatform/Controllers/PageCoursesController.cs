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
    public class PageCoursesController : Controller
    {
        private readonly ELearningPlatformContext _context;

        public PageCoursesController(ELearningPlatformContext context)
        {
            _context = context;
        }

        // GET: PageCourses
        public async Task<IActionResult> Index()
        {
            return View(await _context.PageCourse.ToListAsync());
        }

        // GET: PageCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageCourse = await _context.PageCourse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageCourse == null)
            {
                return NotFound();
            }

            return View(pageCourse);
        }

        // GET: PageCourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PageCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Desc,IdCourse,DateCreation")] PageCourse pageCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pageCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pageCourse);
        }

        // GET: PageCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageCourse = await _context.PageCourse.FindAsync(id);
            if (pageCourse == null)
            {
                return NotFound();
            }
            return View(pageCourse);
        }

        // POST: PageCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Desc,IdCourse,DateCreation")] PageCourse pageCourse)
        {
            if (id != pageCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageCourseExists(pageCourse.Id))
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
            return View(pageCourse);
        }

        // GET: PageCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageCourse = await _context.PageCourse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageCourse == null)
            {
                return NotFound();
            }

            return View(pageCourse);
        }

        // POST: PageCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pageCourse = await _context.PageCourse.FindAsync(id);
            _context.PageCourse.Remove(pageCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageCourseExists(int id)
        {
            return _context.PageCourse.Any(e => e.Id == id);
        }
    }
}
