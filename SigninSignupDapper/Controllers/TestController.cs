
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SigninSignupDapper.Models;
using System.Security.Claims;

namespace SigninSignupDapper.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginVM User)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", User.Email);
            parameters.Add("@Password", User.Password);
            var CurrentUser = DapperORM.DapperORM.ReturnList<User>("Login", parameters).FirstOrDefault();
            if (CurrentUser != null )
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, CurrentUser.Email),
                    new Claim(ClaimTypes.Name, CurrentUser.FirstName),
                    new Claim(ClaimTypes.Name, CurrentUser.LastName),
                    new Claim(ClaimTypes.PrimarySid, CurrentUser.ID.ToString())
                };
                var identity = new ClaimsIdentity(claims, "MyCookie");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("MyCookie", claimsPrincipal);
                return RedirectToAction("Index", "Home");
            }
             return View("Error");
     }
    }
}
