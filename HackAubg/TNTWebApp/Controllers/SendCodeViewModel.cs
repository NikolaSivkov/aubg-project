using System.Collections.Generic;
using System.Web.Mvc;

namespace TNTWebApp.Controllers
{
    public class SendCodeViewModel
    {
        public List<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
        public string SelectedProvider { get; internal set; }
    }
}