#nullable disable

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BlazorEmployee.Areas.Identity.Pages.Role
{
    public class EditRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRoleStore<IdentityRole> _roleStore;
        private readonly ILogger<CreateRoleModel> _logger;

        public EditRoleModel(
            RoleManager<IdentityRole> roleManger,
            UserManager<IdentityUser> userManger,
            IRoleStore<IdentityRole> roleStore,
            ILogger<CreateRoleModel> logger)
        {
            _roleManager = roleManger;
            _userManager = userManger;
            _roleStore = roleStore;
            _logger = logger;
        }
        [BindProperty]
        public IdentityRole roleToEdit { get; set; }
        public List<IdentityUser> usersInRole = new List<IdentityUser>();
        public string ReturnUrl { get; set; }


        public async Task<IActionResult> OnGetAsync(string id, string returUrl = null)
        {
            ReturnUrl = returUrl;
            roleToEdit = await _roleManager.FindByIdAsync(id);
            if(roleToEdit == null)
            {
                return NotFound();
            }
            foreach( var user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user, roleToEdit.Name))
                {
                    usersInRole.Add(user);
                }
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Identity/Pages/Role/Roles");
            if (ModelState.IsValid)
            {
                var result = await _roleManager.UpdateAsync(roleToEdit);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Role updated");
                    return LocalRedirect(returnUrl);
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}
