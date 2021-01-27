using System;
using MvcLaundry.Models;  
using Microsoft.AspNetCore.Authorization;  
using Microsoft.AspNetCore.Authorization.Infrastructure;  
using System.Linq;  
using System.Threading.Tasks; 
using MvcLaundry.Data;
using Microsoft.EntityFrameworkCore;


namespace MvcLaundry.CustomHandler
{
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler  
    {  
        private readonly MvcLaundryContext _dbContext;

        public RolesAuthorizationHandler(MvcLaundryContext identityTenantDbContext){
            _dbContext = identityTenantDbContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,  
                                                       RolesAuthorizationRequirement requirement)  
        {  
            if (context.User == null || !context.User.Identity.IsAuthenticated)  
            {  
                context.Fail();  
                return;
                //return Task.CompletedTask;  
            }  
  
            var validRole = false;  
            if (requirement.AllowedRoles == null ||  
                requirement.AllowedRoles.Any() == false)  
            {  
                validRole = true;  
            }  
            else  
            {  
                var claims = context.User.Claims;  
                var userName = claims.FirstOrDefault(c => c.Type == "UserName").Value;  
                var roles = requirement.AllowedRoles;  
                var user = await _dbContext.Users
                .FirstOrDefaultAsync(p => roles.Contains(p.Role) && p.UserName == userName);
                
                if(user == null){
                    validRole = false;
                }else{
                    validRole = true;
                }
                // validRole = await userManager
  
                // validRole = new Users().GetUsers().Where(p => roles.Contains(p.Role) && p.UserName == userName).Any();  
            }  
  
            if (validRole)  
            {  
                context.Succeed(requirement);  
            }  
            else  
            {  
                context.Fail();  
            }  
          //  return Task.CompletedTask;  
          //return;
        }  
    }  
}
