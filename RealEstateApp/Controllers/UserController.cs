using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return View();
        }

        public IActionResult Register()
        {
            ViewBag.Roles = _roleService.GetAllRoles();
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public IActionResult Register(SaveUserViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _roleService.GetAllRoles();
                return View(saveViewModel);
            }

            return View();
        }
    }
}