using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.Upgrade;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class UpgradeService : GenericService<Upgrade, UpgradeViewModel, SaveUpgradeViewModel>, IUpgradeService
    {
        private readonly IUpgradeRepository _upgradeRepository;
        private readonly IMapper _mapper;

        public UpgradeService(IUpgradeRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _upgradeRepository = repo;
            _mapper = mapper;
        }
    }
}