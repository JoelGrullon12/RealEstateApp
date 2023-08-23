using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public UserService(IAccountService repo, IMapper mapper, IRoleService roleService, IPropertyService propertyService)
        {
            _accountService = repo;
            _mapper = mapper;
            _roleService = roleService;
            _propertyService = propertyService;
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

            if (saveViewModel.Role == Roles.Admin.ToString())
                response = await _accountService.RegisterAdminUserAsync(request);
            else if (saveViewModel.Role == Roles.Client.ToString())
                response = await _accountService.RegisterClientUserAsync(request);
            else if (saveViewModel.Role == Roles.Agent.ToString())
                response = await _accountService.RegisterAgentUserAsync(request);
            else if (saveViewModel.Role == Roles.Developer.ToString())
                response = await _accountService.RegisterDeveloperAsync(request);
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

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(string id)
        {
            SaveUserViewModel user = await _accountService.GetByIdSaveUserViewModel(id);
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

        public async Task Delete(string id)
        {
            SaveUserViewModel user = await _accountService.GetByIdSaveUserViewModel(id);

            if (user.Role == Roles.Agent.ToString())
            {
                List<PropertyViewModel> properties = await _propertyService.GetAllViewModel();
                List<PropertyViewModel> propertiesOfAgentUser = properties.FindAll(property => property.AgentId == user.Id);

                foreach (PropertyViewModel property in propertiesOfAgentUser)
                {
                    await _propertyService.Delete(property.Id);
                }
            }

            await _accountService.DeleteUser(id);
        }

        public async Task LogOut()
        {
            await _accountService.SignOutAsync();
        }
    }
}