using Microsoft.AspNet.Identity;
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
        
        public async Task<ActionResult> Index(int id=0)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["userid"] = User.Identity.GetUserId();
            }
            ViewData["sessionId"] = id;
            return  View();
        }
    }
}