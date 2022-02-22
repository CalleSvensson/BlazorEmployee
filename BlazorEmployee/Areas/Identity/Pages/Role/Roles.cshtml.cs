#nullable disable
using BlazorEmployee.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlazorEmployee.Areas.Identity.Pages.Role
{
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleStore<IdentityRole> _roleStore;
        private readonly ILogger<RegisterModel> _logger;

        public RolesModel(
            RoleManager<IdentityRole> roleManger,
            IRoleStore<IdentityRole> roleStore,
            ILogger<RegisterModel> logger)
        {
            _roleManager = roleManger;
            _roleStore = roleStore;
            _logger = logger;
        }

        public List<IdentityRole> roles = new List<IdentityRole>();
        public string ReturnUrl { get; set; }
        public async Task OnGetAsync(string returUrl = null)
        {
            ReturnUrl = returUrl;
            roles = await _roleManager.Roles.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(
            string id,
            string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Identity/Pages/Role/Roles");
            var roleToDelete = await _roleManager.FindByIdAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            var result = await _roleManager.DeleteAsync(roleToDelete);
            if (result.Succeeded)
            {
                _logger.LogInformation("Role deleted");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return LocalRedirect(returnUrl);
        }
    }
}
