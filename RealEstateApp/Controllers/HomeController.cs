using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.ViewModels.PropertyType;
using RealEstateApp.Core.Application.ViewModels.SellType;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace RealEstateApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISellTypeService _sellTypeService;

        public HomeController(IPropertyService propertyService, IPropertyTypeService propertyTypeService, ISellTypeService sellTypeService)
        {

            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _sellTypeService = sellTypeService;

        }

        public async Task<IActionResult> Index()
        {

            //var properties = 
            /* List<PropertyViewModel> properties = await _propertyService.GetAllViewModel();
            var properties = PropertyViewModel.inc
             //var types = ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
             /*var sells = ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
             List<PropertyViewModel> properties = await _propertyService.GetAllViewModel();
             var propertiesFiltered = properties.FindAll(prop => types = sells).ToList();
             return View(propertiesFiltered);*/

            //var properties = new List<PropertyViewModel>();

            //get all students whose name is Bill




            /*foreach (var student in result)
                Console.WriteLine(properties);*/
            //List<PropertyViewModel> propertiesFiltered = properties.FindAll(prop => prop.TypeId == prop.Type.Id || prop.SellTypeId == prop.SellType.Id);
            //ViewBag.Properties = propertiesFiltered;
            //return View(new PropertyViewModel());
            // List<PropertyViewModel> properties = await _propertyService.GetAllViewModel();
            //List<PropertyTypeViewModel> propertyTypes = await _propertyTypeService.GetAllViewModel();
            //properties.AddRange(types);*/


            return View(await _propertyService.GetAllViewModel());

        }

        public async Task<IActionResult> Filter(FilterPropertyViewModel vm)
        {

            ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModelWithInclude();
            return View(await _propertyService.GetAllViewModelWithFilters(vm));

        }
    }
}