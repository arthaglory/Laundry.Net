using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; 

namespace MvcLaundry.Models
{
    public class Users
    {
        public int Id { get; set; }  
        public string UserName { get; set; }  
        public string Name { get; set; }  
        public string EmailId { get; set; }  
        public string Password { get; set; }
        public string Role { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}