
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SigninSignupDapper.Models;
using System.Security.Claims;

namespace SigninSignupDapper.Controllers
{
    public class AccountController : BaseController.BaseController
    {
       
        public IActionResult Login()
        {   
            return View();
        }
        [HttpPost]
        public Task<IActionResult> Login(LoginVM user) 
        {
            return LoginProto(user);
        }

        public Task<IActionResult> SignOut()
        {
            
            return SignOutProto(); 
        }
    }
}
