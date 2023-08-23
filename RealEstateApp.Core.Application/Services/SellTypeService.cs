using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.SellType;
using RealEstateApp.Core.Domain.Entities;
using StockApp.Core.Application.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class SellTypeService : GenericService<SellType, SellTypeViewModel, SaveSellTypeViewModel>, ISellTypeService
    {
        private readonly ISellTypeRepository _sellRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LoginResponse _user;
        private readonly IMapper _mapper;

        public SellTypeService(ISellTypeRepository repo, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(repo, mapper)
        {
            _sellRepository = repo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<LoginResponse>("user");
        }

        public async Task<List<SellTypeViewModel>> GetAllViewModelWithInclude()
        {
            List<SellType> sellTypes = await _sellRepository.GetAllWithIncludes(new List<string> { "Properties" });
            List<SellTypeViewModel> viewModelList = _mapper.Map<List<SellTypeViewModel>>(sellTypes);
            return viewModelList;
        }
    }
}