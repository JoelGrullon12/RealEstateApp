using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.PropertyType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class PropertyTypeController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IPropertyService _propertyService;

        public PropertyTypeController(IPropertyTypeService propertyTypeService, IPropertyService propertyService)
        {
            _propertyTypeService = propertyTypeService;
            _propertyService = propertyService;
        }

        public async Task<IActionResult> Index()
        {
            List<PropertyTypeViewModel> propertyTypes = await _propertyTypeService.GetAllViewModelWithInclude();
            return View(propertyTypes);
        }

        public IActionResult Create()
        {
            return View("SavePropertyType", new SavePropertyTypeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePropertyTypeViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
                return View("SavePropertyType", saveViewModel);

            await _propertyTypeService.Add(saveViewModel);
            return RedirectToRoute(new { controller = "PropertyType", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            SavePropertyTypeViewModel saveViewModel = await _propertyTypeService.GetByIdSaveViewModel(id);
            return View("SavePropertyType", saveViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePropertyTypeViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
                return View("SavePropertyType", saveViewModel);

            await _propertyTypeService.Update(saveViewModel, saveViewModel.Id);
            return RedirectToRoute(new { controller = "PropertyType", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _propertyTypeService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SavePropertyTypeViewModel saveViewModel)
        {
            await _propertyTypeService.Delete(saveViewModel.Id);
            List<PropertyViewModel> properties = await _propertyService.GetAllViewModel();
            List<PropertyViewModel> propertiesOfTheCurrentType = properties.FindAll(property => property.TypeId == saveViewModel.Id);
            foreach (var property in propertiesOfTheCurrentType)
            {
                await _propertyService.Delete(property.Id);
            }
            return RedirectToRoute(new { controller = "PropertyType", action = "Index" });
        }
    }
}