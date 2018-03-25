using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TNTWebApp.Models;

namespace TNTWebApp.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private   ApplicationUserManager _userManager;
         

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //,AspNetUserManager<ApplicationUser> userManager
        public CoursesController()
        {
            //_userManager = userManager;
            _context = new ApplicationDbContext();
        }

        // GET: Course
        public async Task<ActionResult> Index()
        {
            ViewData["userId"] = User.Identity.GetUserId();

            
            return View( _context.Courses.ToList());
        }

        // GET: Course/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return  new HttpNotFoundResult();
            }

            var course =  _context.Courses.SingleOrDefault(m => m.Id == id);
            if (course == null)
            {
                return  new HttpNotFoundResult();
            }

            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include ="Subject")] Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

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

                var userId = User.Identity.GetUserId(); ;

                var user = _context.Users.Find(userId);
              
                course.Teacher = user;
                user.Courses.Add(course);
                _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return  new HttpNotFoundResult();
            }

            var course =   _context.Courses.SingleOrDefault(m => m.Id == id);
            if (course == null)
            {
                return  new HttpNotFoundResult();
            }
            ViewData["TeacherId"] = id;
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind(Include ="Id,Subject,TeacherId")] Course course)
        {
            if (id != course.Id)
            {
                return  new HttpNotFoundResult();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(course).State = System.Data.Entity.EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return  new HttpNotFoundResult();
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
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return  new HttpNotFoundResult();
            }

            var course =  _context.Courses.SingleOrDefault(m => m.Id == id);
            if (course == null)
            {
                return  new HttpNotFoundResult();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var course = _context.Courses.SingleOrDefault(m => m.Id == id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        public async Task<ActionResult> Join(int id)
        {
            var user = _context.Users.Find(User.Identity.GetUserId());
            user.Courses.Add(_context.Courses.Find(id));

            _context.SaveChanges();
            return RedirectToAction("Index", "codesession", new { id });
        }

    }
}
