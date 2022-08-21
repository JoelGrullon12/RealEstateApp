﻿using AutoMapper;
using RealEstateApp.Core.Application.DTO.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserService(IAccountService repo, IMapper mapper, IRoleService roleService)
        {
            _accountService = repo;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<LoginResponse> LoginAsync(LoginViewModel login)
        {
            LoginRequest request = _mapper.Map<LoginRequest>(login);
            LoginResponse response = await _accountService.LoginAsync(request);
            return response;
        }

        public async Task<RegisterResponse> Add(SaveUserViewModel saveViewModel)
        {
            RegisterRequest request = _mapper.Map<RegisterRequest>(saveViewModel);
            RegisterResponse response;

            if (saveViewModel.Type == Roles.Admin.ToString())
                response = await _accountService.RegisterAdminUserAsync(request);
            else if (saveViewModel.Type == Roles.Client.ToString())
            {
                response = await _accountService.RegisterClientUserAsync(request);
            }
            else
            {
                response = new()
                {
                    HasError = true,
                    Error = "Error, tipo de usuario desconocido"
                };
            }
            return response;
        }

        public async Task<RegisterResponse> Update(SaveUserViewModel saveViewModel)
        {
            RegisterRequest request = _mapper.Map<RegisterRequest>(saveViewModel);
            RegisterResponse response = await _accountService.UpdateUserAsync(request);
            return response;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            List<UserViewModel> viewModelList = await _accountService.GetAllUsers();
            return viewModelList;
        }

        public async Task<UserViewModel> GetByIdViewModel(string id)
        {
            UserViewModel user = await _accountService.GetUserByIdViewModel(id);
            return user;
        }

        public List<RoleViewModel> GetAllRoles()
        {
            List<RoleViewModel> roles = _roleService.GetAllRoles();
            return roles;
        }

        public async Task SetUserStatus(string id, bool status)
        {
            await _accountService.SetUserStatusAsync(id, status);
        }

        public async Task LogOut()
        {
            await _accountService.SignOutAsync();
        }
    }
}