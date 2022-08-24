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

namespace RealEstateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPropertyService _propertyService;

        public HomeController(ILogger<HomeController> logger, IPropertyService propertyService )
        {
            _logger = logger;
            _propertyService = propertyService;
        }

        public async Task<IActionResult> IndexAsync()
        {

            /* var PropertyTypeViewModel = new List<PropertyTypeViewModel>()
             {
                 new PropertyTypeViewModel {Id = 1, Name = "Apartamento", Description = "Apartamento Descripcion" },
                 new PropertyTypeViewModel {Id = 2, Name = "Casa", Description = "Casa Descripcion" }
             };



             var SellTypeViewModel = new List<SellTypeViewModel>()
             {
                 new SellTypeViewModel {Id = 1, Name = "Alquiler", Description = "Alquiler Descripcion" },
                 new SellTypeViewModel {Id = 2, Name = "Venta", Description = "Venta Descripcion" },
             };*/

            PropertyListViewModel propertyList = new();
            List<PropertyViewModel> properties = await _propertyService.GetProperties();

           /* propertyList.PropertyTypes.Add()


            var propiedades = new PropertyListViewModel
            {
                PropertyTypes = PropertyTypeViewModel,
                SellTypes = SellTypeViewModel
            };*/

     
           

            return View(properties);
         
        }
    }
}