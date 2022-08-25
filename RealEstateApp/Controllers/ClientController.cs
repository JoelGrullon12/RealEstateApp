using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class ClientController : Controller
    {
       
        private readonly IPropertyService _propertyService;

        public ClientController(IPropertyService propertyService)
        {

            _propertyService = propertyService;
        }

        public async Task<IActionResult> Index()
        {

            // List<PropertyViewModel> properties = _propertyService.GetAllProperties();
            //ViewBag.Properties = properties;

            return View(await _propertyService.GetAllViewModel());
        }

        public async Task<IActionResult> Properties()
        {
            return View(await _propertyService.GetAllViewModel());
        }

    }
 
}