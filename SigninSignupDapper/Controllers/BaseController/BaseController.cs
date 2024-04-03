using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SigninSignupDapper.Models;
using System;
using System.Security.Claims;

namespace SigninSignupDapper.Controllers.BaseController
{
    public class BaseController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> LoginProto(LoginVM User)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", User.Email);
            parameters.Add("@Password", User.Password);
            var CurrentUser = DapperORM.DapperORM.ReturnList<User>("Login", parameters).FirstOrDefault();
            if (CurrentUser != null)
            {
                List<Claim> claims = new List<Claim>
                {

                  //  new Claim(ClaimTypes.Email, CurrentUser.Email),
                    new Claim  ("UserID", CurrentUser.ID.ToString()),
                    new Claim("UserEmail", CurrentUser.Email, ClaimValueTypes.String),
                    new Claim("UserFirstName", CurrentUser.FirstName),
                    new Claim("UserLastName",CurrentUser.LastName),
                    new Claim("Department",CurrentUser.Department),
                    new Claim("Date",CurrentUser.Datto, ClaimValueTypes.Date)
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                returnClaims(claimsPrincipal);
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                return RedirectToAction(CurrentUser.Department, "Home");
            }
            return View();
        }
        public async Task<IActionResult> SignOutProto()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login");
        }


        private void returnClaims(ClaimsPrincipal principal)
        {
            var toProp = typeof(CurrentUser).GetProperties();

            var result = new CurrentUser();
            foreach (var claim in principal.Claims)
            {
                var to = toProp.FirstOrDefault(x=>x.Name == claim.Type);
                
               
                if (to == null) 
                {
                    continue;
                }
                var from =  principal.Claims.Where(c => c.Type == claim.Type).Select(c => c.Value).FirstOrDefault();
                var from0 = Convert.ChangeType(from, to.PropertyType);
            if(from != null)
                to.SetMethod.Invoke(result,new[] { from0 });

            }
        }

    }
}
