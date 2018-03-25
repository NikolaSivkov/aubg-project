using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TNTWebApp.Data;
using TNTWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TNTWebApp.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        //,AspNetUserManager<ApplicationUser> userManager
        public CourseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            //_userManager = userManager;
            _context = context;
            _userManager = userManager;
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            ViewData["userId"] = _userManager.GetUserId(User);

            var applicationDbContext = _context.Courses.Include(c => c.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Teacher)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject")] Course course)
        {
            if (ModelState.IsValid)
            {
                //TODO: report bug to microsoft 
                //var user = await _userManager.GetUserAsync(HttpContext.User);
                //var mm = string.Copy(user.Id);
                // course.TeacherId = mm;

                //var user =  _userManager.GetUserAsync(HttpContext.User).Result;
                //var mm = string.Copy(user.Id);
                // course.TeacherId = mm;

                //var userId = await Task.Run(() =>
                //{
                //    return _userManager.GetUserId(HttpContext.User);
                //});
                //course.TeacherId = userId;

                var userId = _userManager.GetUserId(HttpContext.User);
                course.TeacherId = userId;

                _context.Courses.Add(course);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["TeacherId"] = id;
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,TeacherId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["TeacherId"] = course;
            return View(course);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Teacher)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Join(int id)
        {
            var course = _context.Courses.Find(id);
            var userId =   _userManager.GetUserId(User);
            var user = _context.Users.Find(userId);

            _context.Entry(new UserCourse { StudentId = user.Id,CourseId = course.Id }).State = EntityState.Added; 
            _context.SaveChanges();
            return RedirectToAction("Index", "codesession", new { id = course.Id });
        }

    }
}
