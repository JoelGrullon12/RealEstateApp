using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.PropertyType;
using RealEstateApp.Core.Application.ViewModels.PropertyUpgrade;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class PropertyUpgradeService : GenericService<PropertyUpgrade, PropertyUpgradeViewModel, SavePropertyUpgradeViewModel>, IPropertyUpgradeService
    {
        private readonly IPropertyUpgradeRepository _propUpgradeRepository;
        private readonly IMapper _mapper;

        public PropertyUpgradeService(IPropertyUpgradeRepository repo, IMapper mapper):base(repo,mapper)
        {
            _propUpgradeRepository = repo;
            _mapper = mapper;
        }
    }
}
