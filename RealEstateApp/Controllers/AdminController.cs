using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Admin;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllViewModel();

            var props = await _propertyService.GetAllViewModel();
            var agents = users.FindAll(t => t.Role == Roles.Agent.ToString());
            var clients = users.FindAll(t => t.Role == Roles.Client.ToString());
            var devs = users.FindAll(t => t.Role == Roles.Developer.ToString());

            return View(new DashboardViewModel
            {
                Properties = props.Count,

                ActiveAgents = agents.FindAll(a => a.IsActive).Count,
                InactiveAgents = agents.FindAll(a => !a.IsActive).Count,

                ActiveClients = clients.FindAll(a => a.IsActive).Count,
                InactiveClients = clients.FindAll(a => !a.IsActive).Count,

                ActiveDevelopers = devs.FindAll(a => a.IsActive).Count,
                InactiveDevelopers = devs.FindAll(a => !a.IsActive).Count
            });
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