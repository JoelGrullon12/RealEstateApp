﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.PropertyUpgrade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISellTypeService _sellTypeService;
        private readonly IUpgradeService _upgradeService;
        private readonly IUpgradeRepository _upgradeRepository;
        private readonly IPropertyUpgradeService _propUpService;

        public PropertyController(IPropertyService propertyService, IPropertyRepository propertyRepository,
            IPropertyTypeService propertyTypeService, ISellTypeService sellTypeService, IUpgradeService upgradeService,
            IUpgradeRepository upgradeRepository, IPropertyUpgradeService propUpService)
        {
            _propertyService = propertyService;
            _propertyRepository = propertyRepository;
            _propertyTypeService = propertyTypeService;
            _sellTypeService = sellTypeService;
            _upgradeService = upgradeService;
            _upgradeRepository = upgradeRepository;
            _propUpService = propUpService;
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Properties = await _propertyService.GetDetailsById(id);
            ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
            ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
            ViewBag.Upgrades = await _upgradeService.GetAllViewModel();
            return View();
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

            List<IFormFile> files = new List<IFormFile> { saveViewModel.ImageFile1, saveViewModel.ImageFile2, saveViewModel.ImageFile3, saveViewModel.ImageFile4 };
            saveViewModel.ImageUrl1 = "termporary-data";
            SavePropertyViewModel savePropertyViewModel = await _propertyService.Add(saveViewModel);

            if (savePropertyViewModel != null && savePropertyViewModel.Id != 0)
            {
                List<string> imagesPath = UploadImage(files, savePropertyViewModel.Id);
                savePropertyViewModel.ImageUrl1 = imagesPath[0];
                savePropertyViewModel.ImageUrl2 = imagesPath[1];
                savePropertyViewModel.ImageUrl3 = imagesPath[2];
                savePropertyViewModel.ImageUrl4 = imagesPath[3];
                await _propertyService.Update(savePropertyViewModel, savePropertyViewModel.Id);

                if (saveViewModel.Upgrades != null)
                {
                    string[] upgrades = saveViewModel.Upgrades.Split(",");

                    for (int i = 1; i < upgrades.Length; i++)
                    {
                        await _propUpService.Add(new SavePropertyUpgradeViewModel
                        {
                            PropertyId = savePropertyViewModel.Id,
                            UpgradeId = Convert.ToInt32(upgrades[i])
                        });
                    }
                }
            }

            return RedirectToRoute(new { controller = "Agent", action = "Properties" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            SavePropertyViewModel saveViewModel = await _propertyService.GetByIdSaveViewModel(id);
            ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
            ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
            ViewBag.Upgrades = await _upgradeService.GetAllViewModel();

            var upgrades = await _propUpService.GetPropertyUpgrades(id);

            foreach (var upgrade in upgrades)
            {
                saveViewModel.Upgrades += $",{upgrade.UpgradeId}";
            }
            return View("SaveProperty", saveViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePropertyViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
                ViewBag.SellTypes = await _sellTypeService.GetAllViewModel();
                ViewBag.Upgrades = await _upgradeService.GetAllViewModel();
                return View("SaveProperty", saveViewModel);
            }

            List<IFormFile> files = new List<IFormFile> { saveViewModel.ImageFile1, saveViewModel.ImageFile2, saveViewModel.ImageFile3, saveViewModel.ImageFile4 };
            SavePropertyViewModel oldSaveViewModel = await _propertyService.GetByIdSaveViewModel(saveViewModel.Id);

            if (saveViewModel != null && saveViewModel.Id != 0)
            {
                List<string> oldImagesPath = new List<string> { oldSaveViewModel.ImageUrl1, oldSaveViewModel.ImageUrl2, oldSaveViewModel.ImageUrl3, oldSaveViewModel.ImageUrl4 };

                List<string> imagesPath = UploadImage(files, saveViewModel.Id, true, oldImagesPath);
                saveViewModel.ImageUrl1 = imagesPath[0] == null ? oldImagesPath[0] : imagesPath[0];
                saveViewModel.ImageUrl2 = imagesPath[1] == null ? oldImagesPath[1] : imagesPath[1];
                saveViewModel.ImageUrl3 = imagesPath[2] == null ? oldImagesPath[2] : imagesPath[2];
                saveViewModel.ImageUrl4 = imagesPath[3] == null ? oldImagesPath[3] : imagesPath[3];
                await _propertyService.Update(saveViewModel, saveViewModel.Id);

                if (saveViewModel.Upgrades != null)
                {
                    var oldUps = await _propUpService.GetPropertyUpgrades(saveViewModel.Id);
                    List<string> newUpgrades = saveViewModel.Upgrades.Split(",").ToList();

                    List<int> toAdd = new();
                    List<int> toDel = new();

                    for (int i = 1; i < newUpgrades.Count; i++)
                    {
                        if (!oldUps.Any(t => t.UpgradeId.ToString() == newUpgrades[i]))
                            toAdd.Add(Convert.ToInt32(newUpgrades[i]));
                    }

                    foreach (var oldUpgrade in oldUps)
                    {
                        if (!newUpgrades.Contains(oldUpgrade.UpgradeId.ToString()))
                            toDel.Add(oldUpgrade.UpgradeId);
                    }

                    foreach (var add in toAdd)
                    {
                        await _propUpService.Add(new SavePropertyUpgradeViewModel
                        {
                            PropertyId = saveViewModel.Id,
                            UpgradeId = add
                        });
                    }

                    foreach (var del in toDel)
                    {
                        await _propUpService.DeleteByPropAndUpgrade(saveViewModel.Id, del);
                    }
                }
                else
                {
                    await _propUpService.DeleteUpgradesByPropertyId(saveViewModel.Id);
                }
            }

            return RedirectToRoute(new { controller = "Agent", action = "Properties" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _propertyService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SavePropertyViewModel saveViewModel)
        {
            string basePath = $"/images/Properties/{saveViewModel.Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new(path);

                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                {
                    directory.Delete();
                }

                Directory.Delete(path);
            }

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