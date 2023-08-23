using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Upgrade;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class UpgradeController : Controller
    {
        private readonly IUpgradeService _upgradeService;
        private readonly IPropertyService _propertyService;

        public UpgradeController(IUpgradeService upgradeService, IPropertyService propertyService)
        {
            _upgradeService = upgradeService;
            _propertyService = propertyService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _upgradeService.GetAllViewModelWithInclude());
        }

        public IActionResult Create()
        {
            return View("SaveUpgrade", new SaveUpgradeViewModel());
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
        }
    }
}