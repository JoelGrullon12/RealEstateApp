using RealEstateApp.Core.Application.DTO.Account;
using RealEstateApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<List<UserViewModel>> GetAllUsers();
        Task<UserViewModel> GetUserByIdViewModel(string id);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterAgentUserAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterClientUserAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterDeveloperAsync(RegisterRequest request);
        Task<SetUserResponse> SetUserStatusAsync(string userId);
        Task<RegisterResponse> UpdateUserAsync(RegisterRequest request);
    }
}