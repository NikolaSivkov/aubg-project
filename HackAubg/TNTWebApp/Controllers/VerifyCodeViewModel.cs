namespace TNTWebApp.Controllers
{
    public class VerifyCodeViewModel
    {
        public string Provider { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
        public string Code { get; internal set; }
        public bool RememberBrowser { get; internal set; }
    }
}