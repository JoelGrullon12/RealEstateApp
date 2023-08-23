using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using StockApp.Core.Application.Helpers;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class ClientController : Controller
    {      
        private readonly IPropertyService _propertyService;
        private readonly IUserService _userService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISellTypeService _sellTypeService;
        private readonly IUpgradeService _upgradeService;
        private readonly IFavoriteService _favService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoginResponse _user;

        public ClientController(IPropertyService propertyService, IPropertyTypeService propertyTypeService, IUpgradeService upgradeService, 
            ISellTypeService sellTypeService, IUserService userService, IHttpContextAccessor httpContextAccessor, IFavoriteService favoriteService)
        {

            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _sellTypeService = sellTypeService;
            _upgradeService = upgradeService;
            _userService = userService;
            _favService = favoriteService;

            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<LoginResponse>("user");
        }

        public async Task<IActionResult> Index()
        {
            return View(await _propertyService.GetAllWithDetails());
        }

        public async Task<IActionResult> Properties()
        {
            var props = await _propertyService.GetAllWithDetails();
            var favs = props.FindAll(p => p.Favorites.Any(f => f.UserId == _user.Id));
            return View(favs);
        }

        public async Task<IActionResult> AddFavorite(int propId)
        {
            var fav = await _favService.Add(new SaveFavoriteViewModel
            {
                PropertyId = propId,
                UserId = _user.Id
            });

            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> RemoveFavorite(int propId)
        {
            await _favService.DeleteByPropAndUserId(propId, _user.Id);
            return RedirectToAction("Properties");

        }
    }
}