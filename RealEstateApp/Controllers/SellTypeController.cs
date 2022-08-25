using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.SellType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class SellTypeController : Controller
    {
        private readonly ISellTypeService _sellTypeService;
        private readonly IPropertyService _propertyService;

        public SellTypeController(ISellTypeService sellTypeService, IPropertyService propertyService)
        {
            _sellTypeService = sellTypeService;
            _propertyService = propertyService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _sellTypeService.GetAllViewModelWithInclude());
        }

        public IActionResult Create()
        {
            return View("SaveSellType", new SaveSellTypeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveSellTypeViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
                return View("SaveSellType", saveViewModel);

            await _sellTypeService.Add(saveViewModel);
            return RedirectToRoute(new { controller = "SellType", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            SaveSellTypeViewModel saveViewModel = await _sellTypeService.GetByIdSaveViewModel(id);
            return View("SaveSellType", saveViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveSellTypeViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
                return View("SaveSellType", saveViewModel);

            await _sellTypeService.Update(saveViewModel, saveViewModel.Id);
            return RedirectToRoute(new { controller = "SellType", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _sellTypeService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SaveSellTypeViewModel saveViewModel)
        {
            await _sellTypeService.Delete(saveViewModel.Id);
            List<PropertyViewModel> properties = await _propertyService.GetAllViewModel();
            List<PropertyViewModel> propertiesOfTheCurrentSellType = properties.FindAll(property => property.SellTypeId == saveViewModel.Id);
            foreach(var property in propertiesOfTheCurrentSellType)
            {
                await _propertyService.Delete(property.Id);
            }
            return RedirectToRoute(new { controller = "SellType", action = "Index" });
        }
    }
}