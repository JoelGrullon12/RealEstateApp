using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.PropertyUpgrade;
using RealEstateApp.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class PropertyUpgradeService : GenericService<PropertyUpgrade, PropertyUpgradeViewModel, SavePropertyUpgradeViewModel>, IPropertyUpgradeService
    {
        private readonly IPropertyUpgradeRepository _propUpgradeRepository;
        private readonly IMapper _mapper;

        public PropertyUpgradeService(IPropertyUpgradeRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _propUpgradeRepository = repo;
            _mapper = mapper;
        }

        public async Task<List<PropertyUpgradeViewModel>> GetPropertyUpgrades(int id)
        {
            var ups = await _propUpgradeRepository.GetAllAsync();
            var propUps = ups.Where(t => t.PropertyId == id);
            return _mapper.Map<List<PropertyUpgradeViewModel>>(propUps);
        }

        public async Task DeleteByPropAndUpgrade(int propId, int upId)
        {
            var props = await _propUpgradeRepository.GetAllAsync();
            var propUp = props.FirstOrDefault(t => t.PropertyId == propId && t.UpgradeId == upId);
            await _propUpgradeRepository.DeleteAsync(propUp);
        }

        public async Task DeleteUpgradesByPropertyId(int propId)
        {
            var props = await _propUpgradeRepository.GetAllAsync();
            var propUps = props.Where(t => t.PropertyId == propId);

            foreach (var prop in propUps)
            {
                await _propUpgradeRepository.DeleteAsync(prop);
            }
        }
    }
}