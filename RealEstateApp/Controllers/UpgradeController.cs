using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Upgrade;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class UpgradeController : Controller
    {
        private readonly IUpgradeService _upgradeService;

        public UpgradeController(IUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _upgradeService.GetAllViewModel());
        }

        public IActionResult Create()
        {
            return View(new SaveUpgradeViewModel());
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