using System;
using Microsoft.AspNetCore.Authentication;  
using Microsoft.AspNetCore.Mvc;  
using System.Collections.Generic;  
using System.Linq;  
using System.Security.Claims;  
using MvcLaundry.Models;
using MvcLaundry.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace MvcLaundry.Controllers
{
    
    public class LoginController : Controller  
    {  
        private readonly MvcLaundryContext _context;

        public LoginController(MvcLaundryContext context){
            _context = context;
        }

        [HttpGet]  
        public ActionResult UserLogin()  
        {  
            return View();  
        }  
  
        [HttpPost]  
        public async Task<ActionResult> UserLogin([Bind] Users userModal)  
        {  
            // username = anet  
            var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == userModal.UserName);
            // var users = new Users();  
            // var allUsers = users.GetUsers().FirstOrDefault();  
            // if (users.GetUsers().Any(u => u.UserName == user.UserName ))  
            // {
            if( user != null){  
                var userClaims = new List<Claim>()  
                {
                new Claim("UserName", user.UserName),  
                    new Claim(ClaimTypes.Name, user.Name),  
                    new Claim(ClaimTypes.Email, user.EmailId),  
                    new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth),  
                    new Claim(ClaimTypes.Role, user.Role)   
                 };  
  
                var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");  
  
                var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });  
                await HttpContext.SignInAsync(userPrincipal);  

                if(user.Role == "Admin"){
                    return RedirectToAction("Index", "Dashboard"); 
                }
  
                return RedirectToAction("Index", "Home");  
            }  
  
            return View(user);  
            // return RedirectToAction("Index", "Home");
        }  

        [HttpGet]  
        public ActionResult Registrasi()  
        {  
            return View();  
        }  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrasi([Bind("Id,UserName,Name,EmailId,Password,Role,DateOfBirth,Address")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(UserLogin));
            }
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }  
}
