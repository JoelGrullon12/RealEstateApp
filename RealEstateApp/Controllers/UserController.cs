using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.User;
using StockApp.Core.Application.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IPropertyService _propertyService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoginResponse _user;

        public UserController(IUserService userService, IRoleService roleService, IPropertyService propertyService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _roleService = roleService;
            _propertyService = propertyService;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<LoginResponse>("user");
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            LoginResponse response = await _userService.LoginAsync(viewModel);

            if (response != null && response.HasError)
            {
                ModelState.AddModelError("loginError", response.Error);
                return View(viewModel);
            }

            _httpContextAccessor.HttpContext.Session.Set("user", response);

            switch (response.Roles[0])
            {
                case "Agent":
                    return RedirectToRoute(new { controller = "Agent", action = "Index" });
                case "Client":
                    return RedirectToRoute(new { controller = "Client", action = "Index" });
                case "Admin":
                    return RedirectToRoute(new { controller = "Admin", action = "Index" });
                default:
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
        }

        public IActionResult Register()
        {
            List<RoleViewModel> roles = _roleService.GetAllRoles();
            List<RoleViewModel> rolesFiltered = roles.FindAll(role => role.Name == "Client" || role.Name == "Agent");
            ViewBag.Roles = rolesFiltered;
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                List<RoleViewModel> roles = _roleService.GetAllRoles();
                List<RoleViewModel> rolesFiltered = roles.FindAll(role => role.Name == "Client" || role.Name == "Agent");
                ViewBag.Roles = rolesFiltered;
                return View(saveViewModel);
            }

            RegisterResponse response = await _userService.Add(saveViewModel);

            if (response != null && response.HasError)
            {
                ModelState.AddModelError("userError", response.Error);
                List<RoleViewModel> roles = _roleService.GetAllRoles();
                List<RoleViewModel> rolesFiltered = roles.FindAll(role => role.Name == "Client" || role.Name == "Agent");
                ViewBag.Roles = rolesFiltered;
                return View(saveViewModel);
            }

            string id = response.UserId;
            string baseImagePath = $"\\images\\Users\\{id}\\";
            string imageUrl = UploadImage(saveViewModel.ImageFile, baseImagePath);
            saveViewModel.Id = response.UserId;
            saveViewModel.ImgUrl = imageUrl;
            saveViewModel.CurrentPassword = saveViewModel.Password;
            await _userService.Update(saveViewModel);

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> Edit(string id)
        {
            SaveUserViewModel saveViewModel = await _userService.GetByIdSaveViewModel(id);
            return View(saveViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel saveViewModel)
        {
            SaveUserViewModel oldUser = await _userService.GetByIdSaveViewModel(saveViewModel.Id);

            if (!ModelState.IsValid)
            {
                if (oldUser.Role == Roles.Agent.ToString())
                    return View("MyProfile", saveViewModel);
                else if (oldUser.Role == Roles.Admin.ToString() || oldUser.Role == Roles.Developer.ToString())
                    return View("Edit", saveViewModel);
            }

            if (oldUser.Role == Roles.Agent.ToString())
            {
                saveViewModel.CurrentPassword = null;
                saveViewModel.Password = null;
            }
            
            RegisterResponse response = await _userService.Update(saveViewModel);

            if (response != null && response.HasError)
            {
                ModelState.AddModelError("userError", response.Error);
                if (oldUser.Role == Roles.Agent.ToString())
                    return View("MyProfile", saveViewModel);
                else if (oldUser.Role == Roles.Admin.ToString() || oldUser.Role == Roles.Developer.ToString())
                    return View("Edit", saveViewModel);
            }

            if (saveViewModel.ImageFile != null)
            {
                string id = saveViewModel.Id;
                SaveUserViewModel oldSaveViewModel = await _userService.GetByIdSaveViewModel(id);
                string oldImageUrl = oldSaveViewModel.ImgUrl;
                string baseImagePath = $"\\images\\Users\\{id}\\";
                string imageUrl = UploadImage(saveViewModel.ImageFile, baseImagePath, oldImageUrl, true);
                saveViewModel.ImgUrl = imageUrl;
                saveViewModel.CurrentPassword = null;
                saveViewModel.Password = null;
                await _userService.Update(saveViewModel);
            }

            string controller = "";
            string action = "";
            if (oldUser.Role == Roles.Admin.ToString())
            {
                controller = "Admin";
                action = "Admins";
            }
            else if (oldUser.Role == Roles.Agent.ToString())
            {
                controller = "User";
                action = "MyProfile";
            }
            else if (oldUser.Role == Roles.Developer.ToString())
            {
                controller = "Developer";
                action = "Index";
            }

            return RedirectToRoute(new { controller = controller, action = action });
        }

        public async Task<IActionResult> MyProfile()
        {
            SaveUserViewModel user = await _userService.GetByIdSaveViewModel(_user.Id);
            return View(user);
        }

        public async Task<IActionResult> SetUserStatus(string id)
        {
            SaveUserViewModel user = await _userService.GetByIdSaveViewModel(id);
            await _userService.SetUserStatus(user.Id, !user.IsActive);
            string controller = "";
            string action = "";
            if (user.Role == Roles.Admin.ToString())
            {
                controller = "Admin";
                action = "Admins";
            }
            else if (user.Role == Roles.Agent.ToString())
            {
                controller = "Admin";
                action = "Agents";
            }
            else if (user.Role == Roles.Developer.ToString())
            {
                controller = "Developer";
                action = "Index";
            }
            return RedirectToRoute(new { controller = controller, action = action });
        }

        public async Task<IActionResult> Delete(string id)
        {
            SaveUserViewModel user = await _userService.GetByIdSaveViewModel(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SaveUserViewModel user)
        {
            string controller = "";
            string action = "";

            if (user.Role == Roles.Agent.ToString())
            {
                controller = "Admin";
                action = "Agents";
                List<PropertyViewModel> properties = await _propertyService.GetAllViewModel();
                List<PropertyViewModel> propertiesOfTheAgent = properties.FindAll(property => property.AgentId == user.Id);
                foreach(PropertyViewModel property in propertiesOfTheAgent)
                {
                    await _propertyService.Delete(property.Id);
                }
            }
            else if (user.Role == Roles.Admin.ToString())
            {
                controller = "Admin";
                action = "Admins";
            }

            SaveUserViewModel saveViewModel = await _userService.GetByIdSaveViewModel(user.Id);

            if (saveViewModel.ImgUrl != null)
            {
                string directoryOfImages = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images\\Users\\{saveViewModel.Id}\\");

                if (Directory.Exists(directoryOfImages))
                {
                    DirectoryInfo directoryInfo = new(directoryOfImages);

                    foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                    {
                        directory.Delete();
                    }

                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        file.Delete();
                    }

                    Directory.Delete(directoryOfImages);
                }
            }

            await _userService.Delete(user.Id);
            return RedirectToRoute(new { controller = controller, action = action });
        }

        public IActionResult LogOut()
        {
            _userService.LogOut();
            _httpContextAccessor.HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        private string UploadImage(IFormFile file, string basePath, string oldImage = null, bool editMode = false)
        {
            if (editMode && file == null)
            {
                return oldImage;
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string filename = guid + fileInfo.Extension;

            string fullFilePath = Path.Combine(path, filename);
            string relativeFilePath = Path.Combine(basePath, filename);

            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (editMode)
            {
                string[] textSplitted = oldImage.Split("\\");
                oldImage = textSplitted[^1];

                string oldImagePath = Path.Combine(path, oldImage);

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            return relativeFilePath;
        }
    }
}