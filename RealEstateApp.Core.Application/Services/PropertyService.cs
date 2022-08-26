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

        public PropertyService(IPropertyRepository repo, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(repo, mapper)
        {
            _propRepository = repo;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<LoginResponse>("user");
            _mapper = mapper;
        }

        public async Task<List<PropertyViewModel>> GetAllViewModelFromUser()
        {
            List<PropertyViewModel> properties = _mapper.Map<List<PropertyViewModel>>(await _propRepository.GetAllWithIncludes(new List<string> { "Type" }));
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
            var propertyList = await _propRepository.GetAllWithIncludes(new List<string> { "Type" });

            var listViewModels = propertyList.Where(property => property.ClientId == _user.Id).Select(property => new PropertyViewModel
            {
                Id = property.Id,
                Code = property.Code,
                Description = property.Description,
                Bedrooms = property.Bedrooms,
                Bathrooms = property.Bathrooms,
                Size = property.Size,
                TypeId = property.Type.Id
            }).ToList();

            if (filters.TypeId != null)
            {
                listViewModels = listViewModels.Where(property => property.TypeId == filters.TypeId.Value).ToList();
            }

            return listViewModels;
        }

        /*public async Task<List<FavoriteViewModel>> GetFavoriteViews()
        {

        }*/
    }
}