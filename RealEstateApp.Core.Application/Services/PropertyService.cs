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
            List<PropertyViewModel> properties = await base.GetAllViewModel();
            List<PropertyViewModel> propertiesOfUserLoggedIn = properties.FindAll(property => property.AgentId == _user.Id).ToList();
            return propertiesOfUserLoggedIn;

        }

        // No es necesario utilizar este metodo ya que se tiene el GetAllViewModel desde la interfaz
        public async Task<List<PropertyViewModel>> GetProperties()
        {
            List<Property> properties = await _propRepository.GetAllAsync();
            return _mapper.Map<List<PropertyViewModel>>(properties);
        }
    }
}