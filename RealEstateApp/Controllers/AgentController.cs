using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;
using StockApp.Core.Application.Helpers;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IUserService _userService;

        public AgentController(IPropertyService propertyService, IUserService userService)
        {
            _propertyService = propertyService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _propertyService.GetAllViewModelFromUser());
        }

        public async Task<IActionResult> Properties()
        {
            return View(await _propertyService.GetAllViewModelFromUser());
        }
    }
}