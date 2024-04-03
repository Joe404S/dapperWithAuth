using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SigninSignupDapper.Models
{
    public class CurrentUser 
    {
        public  long UserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserDepartment { get; set; }
        public string UserDatto { get; set; }

        

    }
    
}


