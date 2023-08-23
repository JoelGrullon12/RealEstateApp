using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;

namespace RealEstateApp.Infrastructure.Identity.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<RoleViewModel> GetAllRoles()
        {
            List<IdentityRole> roles = _roleManager.Roles.ToList();
            List<RoleViewModel> viewModelList = roles.Select(role => new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();

            return viewModelList;
        }
    }
}