using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoPET3.Repository.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DemoPET3.WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly DemoPET3.Repository.Models.DemoPEContext _context;

        public LoginModel(DemoPET3.Repository.Models.DemoPEContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountUser AccountUser { get; set; }
        public string ErrorMessage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var user = _context
                .AccountUsers
                .FirstOrDefault(a=>a.UserId == AccountUser.UserId
                && a.UserPassword == AccountUser.UserPassword);
            
            // Null => ko tìm thấy trong db
            // !null => Tìm thấy

            if (user == null)
            {
                ErrorMessage = "Invalid user name or password";
                return Page();
            }

            if (user.UserRole != 2)
            {
                ErrorMessage = "You are not allow to use this function!";
                return Page();
            }
            
            // user != null và role = 2
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserId),
                new Claim(ClaimTypes.Role, "Manager")
            };
            
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties{ };
            
            // Cấp cookies và đăng nhập
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                authProperties);

            return RedirectToPage("/Books/Index");
        }
    }
}
