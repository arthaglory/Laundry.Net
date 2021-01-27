using System;
using Microsoft.AspNetCore.Authorization;  
using System.Linq;  
using System.Threading.Tasks;  


namespace MvcLaundry.CustomHandler
{
    public class CustomUserRequireClaim : IAuthorizationRequirement  
    {  
        public string ClaimType { get; }  
        public CustomUserRequireClaim(string claimType)  
        {  
            ClaimType = claimType;  
        }  
    }  
}
