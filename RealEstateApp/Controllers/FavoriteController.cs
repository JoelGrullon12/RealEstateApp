using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class FavoriteController : Controller
    {

        private readonly IPropertyService _propertyService;
        private readonly IUserService _userService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISellTypeService _sellTypeService;
        private readonly IUpgradeService _upgradeService;

        public FavoriteController(IPropertyService propertyService, IPropertyTypeService propertyTypeService, IUpgradeService upgradeService, ISellTypeService sellTypeService, IUserService userService)
        {

            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _sellTypeService = sellTypeService;
            _upgradeService = upgradeService;
            _userService = userService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _propertyService.GetAllViewModelFromClient());
        }
    }
}