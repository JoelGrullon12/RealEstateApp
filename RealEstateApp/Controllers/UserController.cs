using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.User;
using StockApp.Core.Application.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoginResponse _user;

        public UserController(IUserService userService, IRoleService roleService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _roleService = roleService;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<LoginResponse>("user");
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            LoginResponse response = await _userService.LoginAsync(viewModel);

            if (response.HasError)
            {
                ModelState.AddModelError("loginError", response.Error);
                return View(viewModel);
            }

            _httpContextAccessor.HttpContext.Session.Set("user", response);

            switch (response.Roles[0])
            {
                case "Agent":
                    return RedirectToRoute(new { controller = "Agent", action = "Index" });
                case "Client":
                    return RedirectToRoute(new { controller = "Client", action = "Index" });
                case "Admin":
                    return RedirectToRoute(new { controller = "Admin", action = "Index" });
                default:
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
        }

        public IActionResult Register()
        {
            List<RoleViewModel> roles = _roleService.GetAllRoles();
            List<RoleViewModel> rolesFiltered = roles.FindAll(role => role.Name == "Client" || role.Name == "Agent");
            ViewBag.Roles = rolesFiltered;
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _roleService.GetAllRoles();
                return View(saveViewModel);
            }

            RegisterResponse response = await _userService.Add(saveViewModel);

            if (response == null || response.HasError)
            {
                ModelState.AddModelError("userError", response.HasError ? response.Error : "Se ha producido un error inesperado tratando de guardar el usuario");
                List<RoleViewModel> roles = _roleService.GetAllRoles();
                List<RoleViewModel> rolesFiltered = roles.FindAll(role => role.Name == "Client" || role.Name == "Agent");
                ViewBag.Roles = rolesFiltered;
                return View(saveViewModel);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> Edit(string id)
        {
            SaveUserViewModel saveViewModel = await _userService.GetByIdSaveViewModel(id);
            return View(saveViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel saveViewModel)
        {
            SaveUserViewModel oldUser = await _userService.GetByIdSaveViewModel(saveViewModel.Id);

            if (!ModelState.IsValid)
            {
                // ModelState.AddModelError("agentError", "Debes rellenar los campos faltantes");
                if (oldUser.Role == Roles.Agent.ToString())
                {
                    return View("MyProfile", saveViewModel);
                }
                else if (oldUser.Role == Roles.Admin.ToString())
                {
                    return View("Edit", saveViewModel);
                }
            }

            RegisterResponse response = await _userService.Update(saveViewModel);

            if (response != null || response.HasError)
            {
                ModelState.AddModelError("userError", response.Error);
                if (oldUser.Role == Roles.Agent.ToString())
                {
                    return View("MyProfile", saveViewModel);
                }
                else if (oldUser.Role == Roles.Admin.ToString())
                {
                    return View("Edit", saveViewModel);
                }
            }

            string controller = "";
            string action = "";
            if (oldUser.Role == Roles.Admin.ToString())
            {
                controller = "Admin";
                action = "Admins";
            }
            else if (oldUser.Role == Roles.Agent.ToString())
            {
                controller = "User";
                action = "MyProfile";
            }
            else if (oldUser.Role == Roles.Developer.ToString())
            {
                controller = "Developer";
                action = "Index";
            }

            return RedirectToRoute(new { controller = controller, action = action });
        }

        public async Task<IActionResult> MyProfile()
        {
            SaveUserViewModel user = await _userService.GetByIdSaveViewModel(_user.Id);
            return View(user);
        }

        public async Task<IActionResult> SetUserStatus(string id)
        {
            SaveUserViewModel user = await _userService.GetByIdSaveViewModel(id);
            await _userService.SetUserStatus(user.Id, !user.IsActive);
            string controller = "";
            string action = "";
            if (user.Role == Roles.Admin.ToString())
            {
                controller = "Admin";
                action = "Admins";
            }
            else if (user.Role == Roles.Agent.ToString())
            {
                controller = "Admin";
                action = "Agents";
            }
            else if(user.Role == Roles.Developer.ToString())
            {
                controller = "Developer";
                action = "Index";
            }
            return RedirectToRoute(new { controller = controller, action = action });
        }

        public async Task<IActionResult> Delete(string id)
        {
            SaveUserViewModel user = await _userService.GetByIdSaveViewModel(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SaveUserViewModel user)
        {
            string controllerAction = "";

            if (user.Role == "Agent")
            {
                controllerAction = "Agents";
            }
            else if (user.Role == "Admin")
            {
                controllerAction = "Admins";
            }

            await _userService.Delete(user.Id);
            return RedirectToRoute(new { controller = "Admin", action = controllerAction });
        }

        public IActionResult LogOut()
        {
            _userService.LogOut();
            _httpContextAccessor.HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}