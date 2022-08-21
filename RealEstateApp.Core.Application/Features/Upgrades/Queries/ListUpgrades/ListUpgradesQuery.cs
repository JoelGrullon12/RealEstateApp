using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.DTO.API.Upgrades;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Upgrades.Queries.ListUpgrades
{
    /// <summary>
    /// Parametros para listar todas las mejoras
    /// </summary>
    public class ListUpgradesQuery:IRequest<IList<UpgradeResponse>>
    {
    }

    public class ListUpgradesQueryHandler : IRequestHandler<ListUpgradesQuery, IList<UpgradeResponse>>
    {
        private readonly IUpgradeRepository _upRepo;
        private readonly IMapper _mapper;

        public ListUpgradesQueryHandler(IUpgradeRepository upRepo, IMapper mapper)
        {
            _upRepo = upRepo;
            _mapper = mapper;
        }

        public async Task<IList<UpgradeResponse>> Handle(ListUpgradesQuery request, CancellationToken cancellationToken)
        {
            var upgrades = await _upRepo.GetAllAsync();
            return _mapper.Map<List<UpgradeResponse>>(upgrades);
        }
    }
}
