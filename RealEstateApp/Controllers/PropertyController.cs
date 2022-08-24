using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISellTypeService _sellTypeService;
        private readonly IUpgradeService _upgradeService;

        public PropertyController(IPropertyService propertyService, IPropertyTypeService propertyTypeService, ISellTypeService sellTypeService, IUpgradeService upgradeService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _sellTypeService = sellTypeService;
            _upgradeService = upgradeService;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
            ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
            ViewBag.Upgrades = await _upgradeService.GetAllViewModel();
            return View("SaveProperty", new SavePropertyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePropertyViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
                ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
                ViewBag.Upgrades = await _upgradeService.GetAllViewModel();
                return View("SaveProperty", saveViewModel);
            }

            return RedirectToRoute(new { controller = "Agent", action = "Properties" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            SavePropertyViewModel saveViewModel = await _propertyService.GetByIdSaveViewModel(id);
            ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
            ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
            ViewBag.Upgrades = await _upgradeService.GetAllViewModel();
            return View("SaveProperty", saveViewModel);
        }

        public async Task<IActionResult> Edit(SavePropertyViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
                ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
                ViewBag.Upgrades = await _upgradeService.GetAllViewModel();
                return View("SaveProperty", saveViewModel);
            }

            await _propertyService.Update(saveViewModel, saveViewModel.Id);
            return RedirectToRoute(new { controller = "Agent", action = "Properties" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _propertyService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SavePropertyViewModel saveViewModel)
        {
            await _propertyService.Delete(saveViewModel.Id);
            return RedirectToRoute(new { controller = "Agent", action = "Properties" });
        }
    }
}