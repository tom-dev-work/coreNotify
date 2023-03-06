using coreNotify.Core;
using coreNotify.Core.Repositories;
using coreNotify.Core.ViewModels;
using CoreNotify.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace coreNotify.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly SignInManager<NotifyUser> _signInManager;

		public UserController(IUnitOfWork UnitOfWork, SignInManager<NotifyUser> signInManager)
        {
            _unitOfWork = UnitOfWork;
			_signInManager = signInManager;
		}
		[Authorize(Policy = Constants.Policies.RequireAdmin)]
		public IActionResult Index()
        {
            var users = _unitOfWork.User.GetUsers();
            return View(users);
        }
		[Authorize(Policy = Constants.Policies.RequireAdmin)]
		public async Task<IActionResult> Edit(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();
            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
            var roleItems = new List<SelectListItem>();
            foreach(var role in roles)
            {
                bool hasRole = userRoles.Any(ur => ur.Contains(role.Name));
                roleItems.Add(new SelectListItem(role.Name, role.Id, hasRole));
            }
            var vm = new EditUserViewModel { User = user, Roles = roleItems }; 
            return View(vm);
        }
		[Authorize(Policy = Constants.Policies.RequireAdmin)]
		[HttpPost]
        public async Task<IActionResult> EditPost(EditUserViewModel data)
        {
			var user = _unitOfWork.User.GetUser(data.User.Id);
            if (user == null)
            {
                return NotFound();
            }
            user.FirstName = data.User.FirstName;
            user.LastName = data.User.LastName;
            user.Email = data.User.Email;

			var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            foreach (var role in data.Roles)
            {
                var AssignedRole = userRoles.FirstOrDefault(ur => ur == role.Text);
				if (role.Selected)
                {
                    if (AssignedRole == null)
                    {
                        await _signInManager.UserManager.AddToRoleAsync(user, role.Text);
                    }
                } else
                {
                    if (AssignedRole != null)
                    {
						await _signInManager.UserManager.RemoveFromRoleAsync(user, role.Text);
					}
                }
			} 

			_unitOfWork.User.UpdateUser(user);

			return RedirectToAction("Edit", new {id = user.Id});
		}
    }
}
