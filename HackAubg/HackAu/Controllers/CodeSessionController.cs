using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackAu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HackAu.Controllers
{
    public class CodeSessionController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public CodeSessionController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
       
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();
                ViewData["userid"] = user.Id;
            }

            return  View();
        }
    }
}