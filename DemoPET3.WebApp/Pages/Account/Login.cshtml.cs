using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DemoPET3.Repository.Models;
using DemoPET3.Repository.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoPET3.WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IRepository<AccountUser> _repository;

        public LoginModel(IRepository<AccountUser> repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            if(HttpContext.User.Identity!=null && HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Books/Index");
            }
            return Page();
        }

        [BindProperty]
        public AccountUser AccountUser { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Not allow authenticated user to access this page, redirect ...
                if(HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated )
                {
                    return RedirectToPage("/Books/Index");
                }
                return Page();
            }

            var user = ((AccountUserRepository) _repository)
                .Login(AccountUser.UserId, AccountUser.UserPassword);
            

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
            
            // Allowed user: User with Role ID = 2
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserId),
                new Claim(ClaimTypes.Role, "Manager")
            };
            
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties{ };
            
            // Issue cookie
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                authProperties);

            return RedirectToPage("/Index");
        }
    }
}
