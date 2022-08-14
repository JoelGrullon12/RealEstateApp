using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.DTO.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountService(UserManager<AppUser> userMaganer, SignInManager<AppUser> signInManager)
        {
            _userManager = userMaganer;
            _signInManager = signInManager;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            LoginResponse response = new();

            var user = await _userManager.FindByNameAsync(request.UserOrEmail);
            bool email = true;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UserOrEmail);
                email = false;
                if (user == null)
                {
                    response.HasError = true;
                    response.Error = $"No existe existe el usuario '{request.UserOrEmail}'";
                    return response;
                }
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = email
                    ? $"Contraseña incorrecta para el email '{request.UserOrEmail}'"
                    : $"Contraseña incorrecta para el nombre de usuario '{request.UserOrEmail}'";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.Username = user.UserName;

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = roles.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task<SetUserResponse> SetUserStatusAsync(string userId,bool status)
        {
            var response = new SetUserResponse();
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                response.HasError = true;
                response.Error = "Esta cuenta no existe";
            }

            user.EmailConfirmed = status;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = !status ? $"Error tratando de inactivar la cuenta '{user.Email}'." : $"Error tratando de activar la cuenta '{user.Email}'.";
            }

            response.Active = status;
            return response;
        }

        public async Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request)
        {
            var userDTO = await RegisterUserAsync(request);

            if (!userDTO.Response.HasError)
            {
                await _userManager.AddToRoleAsync((AppUser)userDTO.User, Roles.Admin.ToString());
            }

            return userDTO.Response;
        }

        public async Task<RegisterResponse> RegisterClientUserAsync(RegisterRequest request)
        {
            var userDTO = await RegisterUserAsync(request);

            if (!userDTO.Response.HasError)
            {
                await _userManager.AddToRoleAsync((AppUser)userDTO.User, Roles.Client.ToString());
            }

            return userDTO.Response;
        }

        public async Task<RegisterResponse> RegisterAgentUserAsync(RegisterRequest request)
        {
            var userDTO = await RegisterUserAsync(request);

            if (!userDTO.Response.HasError)
            {
                await _userManager.AddToRoleAsync((AppUser)userDTO.User, Roles.Agent.ToString());
            }

            return userDTO.Response;
        }

        public async Task<RegisterResponse> RegisterDeveloperAsync(RegisterRequest request)
        {
            var userDTO = await RegisterUserAsync(request);

            if (!userDTO.Response.HasError)
            {
                await _userManager.AddToRoleAsync((AppUser)userDTO.User, Roles.Developer.ToString());
            }

            return userDTO.Response;
        }

        public async Task<RegisterResponse> UpdateUserAsync(RegisterRequest request)
        {
            RegisterResponse response = new();
            response.HasError = false;

            AppUser user = await _userManager.FindByIdAsync(request.Id);

            if (user == null)
            {
                response.HasError = true;
                response.Error = "Este usuario no existe";
                return response;
            }

            if (request.Email != user.Email)
            {
                AppUser userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

                if (userWithSameEmail != null)
                {
                    response.HasError = true;
                    response.Error = "Este email ya esta siendo usado";
                    return response;
                }
            }

            if (request.UserName != user.UserName)
            {
                AppUser userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);

                if (userWithSameUserName != null)
                {
                    response.HasError = true;
                    response.Error = $"El nombre de usuario '{request.UserName}' ya esta en uso";
                    return response;
                }
            }

            user.Name = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.DNI = request.DNI;
            user.ImgUrl = "imagen XD";

            var result = await _userManager.UpdateAsync(user);
            var passResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.Password);

            if (!result.Succeeded || !passResult.Succeeded)
            {
                response.HasError = true;
                response.Error = "Error tratando de actualizar el usuario";
                return response;
            }

            return response;
        }

        public async Task<UserViewModel> GetUserByIdViewModel(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var role = roles[0];

            UserViewModel saveViewModel = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.Name,
                LastName = user.LastName,
                DNI = user.DNI,
                Email = user.Email,
                Phone = user.PhoneNumber,
                UserName = user.UserName,
                IsActive = user.EmailConfirmed,
                Role = role
            };

            return saveViewModel;
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            List<AppUser> users = _userManager.Users.ToList();
            List<UserViewModel> viewModelList = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                FirstName = user.Name,
                LastName = user.LastName,
                DNI = user.DNI,
                Email = user.Email,
                Phone = user.PhoneNumber,
                UserName = user.UserName,
                IsActive = user.EmailConfirmed
            }).ToList();

            int counter = 0;

            foreach (AppUser user in users)
            {
                var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                string role = roles[0];
                viewModelList[counter].Role = role;
                counter++;
            }

            return viewModelList;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        #region Private Methods
        private async Task<RegisterUserDTO> RegisterUserAsync(RegisterRequest request)
        {
            RegisterUserDTO userDTO = new();
            userDTO.Response.HasError = false;

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);

            if (userWithSameUserName != null)
            {
                userDTO.Response.HasError = true;
                userDTO.Response.Error = $"El nombre de usuario '{request.UserName}' ya esta en uso.";
                return userDTO;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

            if (userWithSameEmail != null)
            {
                userDTO.Response.HasError = true;
                userDTO.Response.Error = $"Ya existe una cuenta registrada con el email '{request.Email}'.";
                return userDTO;
            }

            var user = new AppUser
            {
                Email = request.Email,
                UserName = request.UserName,
                Name = request.FirstName,
                LastName = request.LastName,
                DNI = request.DNI,
                PhoneNumber = request.Phone,
                ImgUrl = "imagen XD"
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                userDTO.Response.HasError = true;
                userDTO.Response.Error = $"Ha ocurrido un error tratando de registrar al usuario";
                return userDTO;
            }

            userDTO.User = user;
            return userDTO;
        }
        #endregion
    }
}
