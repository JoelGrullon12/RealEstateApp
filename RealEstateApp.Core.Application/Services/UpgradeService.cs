using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Upgrade;
using RealEstateApp.Core.Domain.Entities;
using StockApp.Core.Application.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class UpgradeService : GenericService<Upgrade, UpgradeViewModel, SaveUpgradeViewModel>, IUpgradeService
    {
        private readonly IUpgradeRepository _upgradeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoginResponse _user;
        private readonly IMapper _mapper;

        public UpgradeService(IUpgradeRepository repo, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(repo, mapper)
        {
            _upgradeRepository = repo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<LoginResponse>("user");
        }

        public async Task<List<UpgradeViewModel>> GetAllViewModelWithInclude()
        {
            List<Upgrade> upgrades = await _upgradeRepository.GetAllWithIncludes(new List<string> { "Properties" });
            List<UpgradeViewModel> viewModelList = _mapper.Map<List<UpgradeViewModel>>(upgrades);
            return viewModelList;
        }
    }
}