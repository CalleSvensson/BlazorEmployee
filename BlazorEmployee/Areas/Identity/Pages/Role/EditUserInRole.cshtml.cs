#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorEmployee.Areas.Identity.Pages.Role
{
    public class EditUserInRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<CreateRoleModel> _logger;

        public EditUserInRoleModel(
            RoleManager<IdentityRole> roleManger,
            UserManager<IdentityUser> userManger,
            ILogger<CreateRoleModel> logger)
        {
            _roleManager = roleManger;
            _userManager = userManger;
            _logger = logger;
        }
        [BindProperty]
        public List<UserRoleModel> userRoleList { get; set; }
        public IdentityRole role { get; set; }
        public class UserRoleModel
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public bool IsSelected { get; set; }
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            role = await _roleManager.FindByIdAsync(id);
            userRoleList = new List<UserRoleModel>();
            if(role == null)
            {
                return NotFound();
            }

            foreach(var user in _userManager.Users)
            {
                var userRoleModel = new UserRoleModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                   userRoleModel.IsSelected = true;
                }
                else
                {
                   userRoleModel.IsSelected = false;
                }
                userRoleList.Add(userRoleModel);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            for(int i = 0; i < userRoleList.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(userRoleList[i].UserId);

                IdentityResult result = null;

                if(userRoleList[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!userRoleList[i].IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if(result.Succeeded)
                {
                    if (i < (userRoleList.Count - 1))
                        continue;
                    else
                        return LocalRedirect("/Identity/Pages/Role/EditRole/" + id);
                }
            }
            return LocalRedirect("/Identity/Pages/Role/EditRole/" + id);
        }
    }
}
