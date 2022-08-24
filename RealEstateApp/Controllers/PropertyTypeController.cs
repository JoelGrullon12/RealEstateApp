using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class PropertyTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}