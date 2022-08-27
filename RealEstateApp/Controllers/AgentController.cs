using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.PropertyType;
using StockApp.Core.Application.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IUserService _userService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISellTypeService _sellTypeService;
        private readonly IUpgradeService _upgradeService;

        public AgentController(IPropertyService propertyService, IPropertyTypeService propertyTypeService, IUpgradeService upgradeService, ISellTypeService sellTypeService, IUserService userService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _sellTypeService = sellTypeService;
            _upgradeService = upgradeService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _propertyService.GetAllViewModelFromUser());
        }

        public async Task<IActionResult> Properties()
        {
            return View(await _propertyService.GetAllViewModelFromUser());
        }

        public async Task<IActionResult> PropertiesOfTheAgent(string id)
        {
            SaveUserViewModel agent = await _userService.GetByIdSaveViewModel(id);
            List<PropertyViewModel> properties = await _propertyService.GetAllViewModel();
            ViewBag.PropertiesOfTheAgent = properties.FindAll(property => property.AgentId == agent.Id);
            return View(agent);
        }
    }
}