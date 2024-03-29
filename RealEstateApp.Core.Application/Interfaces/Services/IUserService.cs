﻿using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<RegisterResponse> Add(SaveUserViewModel saveViewModel);
        List<RoleViewModel> GetAllRoles();
        Task<List<UserViewModel>> GetAllViewModel();
        Task<SaveUserViewModel> GetByIdSaveViewModel(string id);
        Task<LoginResponse> LoginAsync(LoginViewModel login);
        Task Delete(string id);
        Task LogOut();
        Task SetUserStatus(string id, bool status);
        Task<RegisterResponse> Update(SaveUserViewModel saveViewModel);
    }
}