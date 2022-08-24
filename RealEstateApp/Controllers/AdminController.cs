using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPropertyService _propertyService;

        public AdminController(IUserService userService, IPropertyService propertyService)
        {
            _userService = userService;
            _propertyService = propertyService;
        }

        public IActionResult Index()
        {
            return View();
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
                ModelState.AddModelError("adminError", response.Error);
                return View(saveViewModel);
            }

            await _userService.SetUserStatus(response.UserId, true);
            return RedirectToRoute(new { controller = "Admin", action = "Admins" });
        }

        public async Task<IActionResult> Agents()
        {
            List<UserViewModel> users = await _userService.GetAllViewModel();
            List<UserViewModel> agents = users.FindAll(user => user.Role == Roles.Agent.ToString());
            foreach (UserViewModel agent in agents)
            {
                List<PropertyViewModel> properties = await _propertyService.GetAllViewModel();
                List<PropertyViewModel> propertiesOfCurrentAgent = properties.FindAll(property => property.AgentId == agent.Id);
                agent.PropertiesCount = propertiesOfCurrentAgent.Count;
            }
            return View(agents);
        }

        public async Task<IActionResult> Admins()
        {
            List<UserViewModel> users = await _userService.GetAllViewModel();
            List<UserViewModel> admins = users.FindAll(user => user.Role == Roles.Admin.ToString());
            return View(admins);
        }
    }
}