using RealEstateApp.Core.Application.ViewModels.User;
using System.Collections.Generic;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IRoleService
    {
        List<RoleViewModel> GetAllRoles();
    }
}