#nullable disable

using BlazorEmployee.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlazorEmployee.Pages.User
{
    public class UsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly ILogger<RegisterModel> _logger;

        public UsersModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _userStore = userStore;
            _logger = logger;
        }

        public List<IdentityUser> users = new List<IdentityUser>();
        public string ReturnUrl { get; set; }
        public async Task OnGetAsync(string returUrl = null)
        {
            ReturnUrl = returUrl;
            users = await _userManager.Users.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(
            string id,
            string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/User/Users");
            var userToDelete = await _userManager.FindByIdAsync(id);
            if(User == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(userToDelete);
            if (result.Succeeded)
            {
                _logger.LogInformation("User deleted");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return LocalRedirect(returnUrl);
        }
    }
}
