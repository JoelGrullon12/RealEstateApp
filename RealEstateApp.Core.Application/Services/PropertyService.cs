using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Domain.Entities;
using StockApp.Core.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class PropertyService : GenericService<Property, PropertyViewModel, SavePropertyViewModel>, IPropertyService
    {
        private readonly IPropertyRepository _propRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoginResponse _user;
        private readonly IMapper _mapper;

        public PropertyService(IPropertyRepository repo, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(repo, mapper)
        {
            _propRepository = repo;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<LoginResponse>("user") == null ? new LoginResponse() : _httpContextAccessor.HttpContext.Session.Get<LoginResponse>("user");
            _mapper = mapper;
        }

        public override async Task<SavePropertyViewModel> Add(SavePropertyViewModel vm)
        {
            Random rdm = new Random();
            int code = rdm.Next(999999);
            vm.Code = code;
            return await base.Add(vm);
        }

        public async Task<List<PropertyViewModel>> GetAllViewModelFromUser()
        {
            var props = await _propRepository.GetAllWithIncludes(new List<string> { "Type", "SellType" });
            List<PropertyViewModel> properties = _mapper.Map<List<PropertyViewModel>>(props);
            List<PropertyViewModel> propertiesOfUserLoggedIn = properties.FindAll(property => property.AgentId == _user.Id).ToList();
            return propertiesOfUserLoggedIn;
        }

        /*public async Task<List<PropertyViewModel>> GetAllVieModelById()
        {
            List<PropertyViewModel> properties = _mapper.Map<List<PropertyViewModel>>(await _propRepository.GetByIdAsync(id)).ToList();
            return properties;

        }*/

        public async Task<List<PropertyViewModel>> GetAllViewModelWithFilters(FilterPropertyViewModel filters)
        {
            List<Property> propertyList = await _propRepository.GetAllWithIncludes(new List<string> { "Type" });
            List<PropertyViewModel> listViewModels = _mapper.Map<List<PropertyViewModel>>(propertyList);

            if (filters.TypeId != null)
            {
                listViewModels = listViewModels.Where(property => property.TypeId == filters.TypeId.Value).ToList();
            }

            return listViewModels;
        }

        /*public async Task<List<FavoriteViewModel>> GetFavoriteViews()
        {

        }*/

        public async Task<PropertyViewModel> GetDetailsById(int id)
        {
            var prop = await _propRepository.GetByIdWithIncludes(id, new List<string> { "Type", "SellType" }, new List<string> { "Upgrades" });
            return _mapper.Map<PropertyViewModel>(prop);
        }

        public async Task<List<PropertyViewModel>> GetPropertiesOfAgent(string agentId)
        {
            var props = await _propRepository.GetAllWithIncludes(new List<string> { "Type", "SellType", "Upgrades" });
            var agentProps = props.FindAll(props => props.AgentId == agentId);
            return _mapper.Map<List<PropertyViewModel>>(agentProps);
        }

        public async Task<List<PropertyViewModel>> GetAllWithDetails()
        {
            var props = await _propRepository.GetAllWithIncludes(new List<string> { "Type", "SellType", "Upgrades", "Favorites" });
            return _mapper.Map<List<PropertyViewModel>>(props);
        }
    }
}