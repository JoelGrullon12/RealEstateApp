using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}