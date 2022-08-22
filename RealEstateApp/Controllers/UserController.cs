﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
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

        public UserController(IUserService userService, IRoleService roleService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _roleService = roleService;
            _httpContextAccessor = httpContextAccessor;
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
        public IActionResult Register(SaveUserViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _roleService.GetAllRoles();
                return View(saveViewModel);
            }

            return View();
        }

        public IActionResult LogOut()
        {
            _userService.LogOut();
            _httpContextAccessor.HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}