﻿using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}