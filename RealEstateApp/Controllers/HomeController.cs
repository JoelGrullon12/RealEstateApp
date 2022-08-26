using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.PropertyType;
using RealEstateApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISellTypeService _sellTypeService;
        private readonly IUpgradeService _upgradeService;
        private readonly IUserService _userService;

        public HomeController(IPropertyService propertyService, IPropertyTypeService propertyTypeService, IUpgradeService upgradeService, IUserService userService, ISellTypeService sellTypeService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _sellTypeService = sellTypeService;
            _upgradeService = upgradeService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
            ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
            ViewBag.Upgrades = await _upgradeService.GetAllViewModel();
            ViewBag.Properties = await _propertyService.GetAllViewModel();
            return View(); 
        }

        public async Task<IActionResult> Filter(FilterPropertyViewModel vm)
        {


           // ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModelWithInclude();

            return View(await _propertyService.GetAllViewModelWithFilters(vm));
        }

        public async Task<IActionResult> Agents()
        {
            List<UserViewModel> users = await _userService.GetAllViewModel();
            List<UserViewModel> agents = users.FindAll(user => user.Role == Roles.Agent.ToString());
            return View(agents);
        }
    }
}