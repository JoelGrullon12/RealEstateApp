using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Domain.Settings;
using RealEstateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace RealEstateApp.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JWTSettings _jwtSettings;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JWTSettings> jWTSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jWTSettings.Value;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            LoginResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.UserOrEmail);
            bool email = true;

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.UserOrEmail);
                email = false;
                if (user == null)
                {
                    response.HasError = true;
                    response.Error = $"No existe el usuario '{request.UserOrEmail}'";
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

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            response.Id = user.Id;
            response.Email = user.Email;
            response.Username = user.UserName;

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = roles.ToList();
            response.IsVerified = user.EmailConfirmed;

            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public async Task<SetUserResponse> SetUserStatusAsync(string userId, bool status)
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

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.DNI = request.DNI;
            user.ImgUrl = "";

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

        public async Task<SaveUserViewModel> GetByIdSaveUserViewModel(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var role = roles[0];

            SaveUserViewModel saveViewModel = new SaveUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
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
                FirstName = user.FirstName,
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

        public async Task DeleteUser(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        #region Private Methods
        private async Task<RegisterUserDTO> RegisterUserAsync(RegisterRequest request)
        {
            RegisterUserDTO userDTO = new();
            userDTO.Response = new RegisterResponse();
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
                FirstName = request.FirstName,
                LastName = request.LastName,
                DNI = request.DNI,
                PhoneNumber = request.Phone,
                ImgUrl = ""
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                userDTO.Response.HasError = true;
                userDTO.Response.Error = $"Ha ocurrido un error tratando de registrar al usuario";
                return userDTO;
            }

            userDTO.User = user;
            userDTO.Response.UserId = user.Id;
            return userDTO;
        }

        private async Task<JwtSecurityToken> GenerateJWToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("userId",user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            string key = _jwtSettings.Key;

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(2),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
        #endregion
    }
}