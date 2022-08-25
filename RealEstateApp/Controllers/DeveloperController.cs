using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly IUserService _userService;

        public DeveloperController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            List<UserViewModel> users = await _userService.GetAllViewModel();
            List<UserViewModel> developers = users.FindAll(user => user.Role == Roles.Developer.ToString());
            return View(developers);
        }

        public IActionResult Create()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(saveViewModel);
            }

            RegisterResponse response = await _userService.Add(saveViewModel);

            if (response != null && response.HasError)
            {
                ModelState.AddModelError("developerError", response.Error);
                return View(saveViewModel);
            }

            await _userService.SetUserStatus(response.UserId, true);
            return RedirectToRoute(new { controller = "Developer", action = "Index" });
        }
    }
}