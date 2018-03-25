﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TNTWebApp.Models;

namespace TNTWebApp.Controllers
{
    public class CodeSessionController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
          
        public CodeSessionController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
       
        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["userid"] = User.Identity.GetUserId();
            }

            return  View();
        }
    }
}