﻿using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}