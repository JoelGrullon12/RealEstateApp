using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using System.Collections.Generic;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class ClientController : Controller
    {
       
        private readonly IPropertyService _propertyService;

        public ClientController(IPropertyService propertyService)
        {

            _propertyService = propertyService;
        }

        public IActionResult Index()
        {

            // List<PropertyViewModel> properties = _propertyService.GetAllProperties();
            //ViewBag.Properties = properties;

           
            return View(new PropertyViewModel());
        }

    }
    /*public IActionResult Register()
{
    List<RoleViewModel> roles = _roleService.GetAllRoles();
    List<RoleViewModel> rolesFiltered = roles.FindAll(role => role.Name == "Client" || role.Name == "Agent");
    ViewBag.Roles = rolesFiltered;
    return View(new SaveUserViewModel());
}*/
}