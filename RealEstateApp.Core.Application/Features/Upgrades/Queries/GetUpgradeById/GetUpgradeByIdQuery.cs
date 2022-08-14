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

namespace RealEstateApp.Core.Application.Features.Upgrades.Queries.GetUpgradeById
{
    public class GetUpgradeByIdQuery:IRequest<UpgradeResponse>
    {
        public int Id { get; set; }
    }

    public class GetUpgradeByIdQueryHandler : IRequestHandler<GetUpgradeByIdQuery, UpgradeResponse>
    {
        private readonly IUpgradeRepository _upRepo;
        private readonly IMapper _mapper;

        public GetUpgradeByIdQueryHandler(IUpgradeRepository upRepo, IMapper mapper)
        {
            _upRepo = upRepo;
            _mapper = mapper;
        }

        public async Task<UpgradeResponse> Handle(GetUpgradeByIdQuery request, CancellationToken cancellationToken)
        {
            var upgrade = await _upRepo.GetByIdAsync(request.Id);
            return _mapper.Map<UpgradeResponse>(upgrade);
        }
    }

}
