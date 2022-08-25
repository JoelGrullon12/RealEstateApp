using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
<<<<<<< HEAD
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.Upgrade;
using System.Collections.Generic;
=======
using RealEstateApp.Core.Application.ViewModels.Upgrade;
>>>>>>> f83ea9464c84dd770c5d733617ca348cbb61e76c
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class UpgradeController : Controller
    {
        private readonly IUpgradeService _upgradeService;
<<<<<<< HEAD
        private readonly IPropertyService _propertyService;

        public UpgradeController(IUpgradeService upgradeService, IPropertyService propertyService)
        {
            _upgradeService = upgradeService;
            _propertyService = propertyService;
=======

        public UpgradeController(IUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
>>>>>>> f83ea9464c84dd770c5d733617ca348cbb61e76c
        }

        public async Task<IActionResult> Index()
        {
<<<<<<< HEAD
            return View(await _upgradeService.GetAllViewModelWithInclude());
=======
            return View(await _upgradeService.GetAllViewModel());
>>>>>>> f83ea9464c84dd770c5d733617ca348cbb61e76c
        }

        public IActionResult Create()
        {
<<<<<<< HEAD
            return View("SaveUpgrade", new SaveUpgradeViewModel());
=======
            return View(new SaveUpgradeViewModel());
>>>>>>> f83ea9464c84dd770c5d733617ca348cbb61e76c
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveUpgradeViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
                return View("SaveUpgrade", saveViewModel);

            await _upgradeService.Add(saveViewModel);
            return RedirectToRoute(new { controller = "Upgrade", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
<<<<<<< HEAD
        {
            SaveUpgradeViewModel saveViewModel = await _upgradeService.GetByIdSaveViewModel(id);
            return View("SaveUpgrade", saveViewModel);
=======
        {
            return View("SaveUpgrade", await _upgradeService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUpgradeViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
                return View("SaveUpgrade", saveViewModel);

            await _upgradeService.Update(saveViewModel, saveViewModel.Id);
            return RedirectToRoute(new { controller = "Upgrade", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _upgradeService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SaveUpgradeViewModel saveViewModel)
        {
            await _upgradeService.Delete(saveViewModel.Id);
            return RedirectToRoute(new { controller = "Upgrade", action = "Index" });
>>>>>>> f83ea9464c84dd770c5d733617ca348cbb61e76c
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUpgradeViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
                return View("SaveUpgrade", saveViewModel);

            await _upgradeService.Update(saveViewModel, saveViewModel.Id);
            return RedirectToRoute(new { controller = "Upgrade", action = "Index" });
        }

       /* public async Task<IActionResult> Delete(int id)
        {
            return View(await _upgradeService.GetByIdSaveViewModel(id));
        }*/

       // [HttpPost]
       /* public async Task<IActionResult> Delete(SaveUpgradeViewModel saveViewModel)
        {
            await _upgradeService.Delete(saveViewModel.Id);
            List<PropertyViewModel> properties = await _propertyService.GetAllViewModel();
            List<PropertyViewModel> propertiesOfTheCurrentUpgrade = properties.FindAll(property => property.Upgrades = saveViewModel.Id);
            foreach (var property in propertiesOfTheCurrentUpgrade)
            {
                await _propertyService.Delete(property.Id);
            }
            return RedirectToRoute(new { controller = "SellType", action = "Index" });
        }*/
    }
}