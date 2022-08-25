﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISellTypeService _sellTypeService;
        private readonly IUpgradeService _upgradeService;

        public PropertyController(IPropertyService propertyService, IPropertyTypeService propertyTypeService, ISellTypeService sellTypeService, IUpgradeService upgradeService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _sellTypeService = sellTypeService;
            _upgradeService = upgradeService;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
            ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
            ViewBag.Upgrades = await _upgradeService.GetAllViewModel();
            return View("SaveProperty", new SavePropertyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePropertyViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
                ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
                ViewBag.Upgrades = await _upgradeService.GetAllViewModel();
                return View("SaveProperty", saveViewModel);
            }

            // Save property and related images

            return RedirectToRoute(new { controller = "Agent", action = "Properties" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            SavePropertyViewModel saveViewModel = await _propertyService.GetByIdSaveViewModel(id);
            ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
            ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
            ViewBag.Upgrades = await _upgradeService.GetAllViewModel();
            return View("SaveProperty", saveViewModel);
        }

        public async Task<IActionResult> Edit(SavePropertyViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
                ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
                ViewBag.Upgrades = await _upgradeService.GetAllViewModel();
                return View("SaveProperty", saveViewModel);
            }

            await _propertyService.Update(saveViewModel, saveViewModel.Id);
            return RedirectToRoute(new { controller = "Agent", action = "Properties" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _propertyService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SavePropertyViewModel saveViewModel)
        {
            await _propertyService.Delete(saveViewModel.Id);
            return RedirectToRoute(new { controller = "Agent", action = "Properties" });
        }

        private List<string> UploadImage(List<IFormFile> files, int id, bool isEditMode = false, List<string> oldImagesPath = null)
        {
            if (isEditMode && files == null)
            {
                return oldImagesPath;
            }

            List<string> relativeImagesPath = new();

            string basePath = $"/images/Properties/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (isEditMode)
            {
                int counter = 0;

                foreach (IFormFile file in files)
                {
                    string relativeFilename = null;

                    if (file != null)
                    {
                        Guid guid = Guid.NewGuid();
                        FileInfo fileInfo = new(file.FileName);
                        string filename = guid + fileInfo.Extension;

                        string fullPathName = Path.Combine(path, filename);

                        using (var stream = new FileStream(fullPathName, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        relativeFilename = $"{basePath}/{filename}";
                        relativeImagesPath.Add(relativeFilename);

                        string imageUrl = oldImagesPath[counter];

                        if (imageUrl != null)
                        {
                            System.IO.File.Delete($"{Directory.GetCurrentDirectory()}/wwwroot{imageUrl}");
                        }
                    }
                    else
                    {
                        relativeImagesPath.Add(relativeFilename);
                    }

                    counter++;
                }
            }
            else
            {
                foreach (IFormFile file in files)
                {
                    string relativeFilename = null;

                    if (file != null)
                    {
                        Guid guid = Guid.NewGuid();
                        FileInfo fileInfo = new(file.FileName);
                        string filename = guid + fileInfo.Extension;

                        string fullPathName = Path.Combine(path, filename);

                        using (var stream = new FileStream(fullPathName, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        relativeFilename = $"{basePath}/{filename}";
                        relativeImagesPath.Add(relativeFilename);
                    }
                    else
                    {
                        relativeImagesPath.Add(relativeFilename);
                    }
                }
            }

            return relativeImagesPath;
        }
    }
}