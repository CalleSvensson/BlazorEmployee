#nullable disable
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BlazorEmployee.Areas.Identity.Pages.Role
{
    public class CreateRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleStore<IdentityRole> _roleStore;
        private readonly ILogger<CreateRoleModel> _logger;

        public CreateRoleModel(
            RoleManager<IdentityRole> roleManger,
            IRoleStore<IdentityRole> roleStore,
            ILogger<CreateRoleModel> logger)
        {
            _roleManager = roleManger;
            _roleStore = roleStore;
            _logger = logger;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

        }
        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var role = CreateRole();
                await _roleStore.SetRoleNameAsync(role, Input.Name, CancellationToken.None);
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    _logger.LogInformation("New role created");
                    return LocalRedirect(returnUrl);
                }
            }
            return Page();
        }   

        private IdentityRole CreateRole()
        {
            try
            {
                return Activator.CreateInstance<IdentityRole>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityRole)}'. " +
                    $"Ensure that '{nameof(IdentityRole)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Role/CreateRole.cshtml");
            }
        }
    }
}
