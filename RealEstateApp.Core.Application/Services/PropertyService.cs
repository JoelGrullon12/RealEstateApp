using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Domain.Entities;
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
        private readonly IMapper _mapper;

        public PropertyService(IPropertyRepository repo, IMapper mapper):base(repo,mapper)
        {
            _propRepository = repo;
            _mapper = mapper;
        }

        public async Task<List<PropertyViewModel>> GetProperties()
        {
           
           List<Property> properties = await _propRepository.GetAllAsync();
            return _mapper.Map<List<PropertyViewModel>>(properties);
        }
    }
}
