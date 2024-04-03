using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SigninSignupDapper.Autherization
{
    public class AutherizationRequirements : IAuthorizationRequirement
    {
        public AutherizationRequirements(int probation)
        {

            Probation = probation;

        }
        public int Probation { get; }
    }
    public class AutherizationRequirementsHandler : AuthorizationHandler<AutherizationRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AutherizationRequirements requirement)
        {
            
            if (!context.User.HasClaim(m => m.Type == "Date")) 
                return Task.CompletedTask;
            var userDate = DateTime.Parse (context.User.FindFirst(m => m.Type == "Date").Value);
            var period = DateTime.Now - userDate;
            if (period.Days > 30 * requirement.Probation)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

}
