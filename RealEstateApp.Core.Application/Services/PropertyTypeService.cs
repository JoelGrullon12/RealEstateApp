﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.PropertyType;
using RealEstateApp.Core.Domain.Entities;
using StockApp.Core.Application.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class PropertyTypeService : GenericService<PropertyType, PropertyTypeViewModel, SavePropertyTypeViewModel>, IPropertyTypeService
    {
        private readonly IPropertyTypeRepository _propTypeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoginResponse _user;
        private readonly IMapper _mapper;

        public PropertyTypeService(IPropertyTypeRepository repo, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(repo, mapper)
        {
            _propTypeRepository = repo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<LoginResponse>("user");
            _mapper = mapper;
        }

        public async Task<List<PropertyTypeViewModel>> GetAllViewModelWithInclude()
        {
            List<PropertyType> propertyTypes = await _propTypeRepository.GetAllWithIncludes(new List<string> { "Properties" });
            List<PropertyTypeViewModel> viewModelList = _mapper.Map<List<PropertyTypeViewModel>>(propertyTypes);
            return viewModelList;
        }
    }
}