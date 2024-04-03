using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigninSignupDapper.Models;
using System.Diagnostics;

namespace SigninSignupDapper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            Dictionary<string, string> dept = User.Claims.Where(claim => claim.Type == "Department").ToDictionary(claim => claim.Type, claim => claim.Value);


            return View(dept);
        }
        [Authorize(Policy= "hrpolicy")]
        public IActionResult HR()
        {
            return View();
        }
		[Authorize(Policy = "marketingpolicy")]
		public IActionResult marketing()
		{
			return View();
		}
        [Authorize(Policy ="NullException")]
        public IActionResult NULL()
        {
            return View();
        }
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
