using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using StockApp.Core.Application.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IFavoriteService _favService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoginResponse _user;

        public FavoriteController(IPropertyService propertyService, IFavoriteService favoriteService, IHttpContextAccessor httpContext)
        {
            _propertyService=propertyService;
            _favService = favoriteService;
            _httpContextAccessor=httpContext;
            _user = _httpContextAccessor.HttpContext.Session.Get<LoginResponse>("user");
        }

        public async Task<IActionResult> Index()
        {
            var props = await _propertyService.GetAllWithDetails();
            var favs = props.FindAll(p => p.Favorites.Any(f => f.UserId == _user.Id));
            return View(favs);
        }
    }
}