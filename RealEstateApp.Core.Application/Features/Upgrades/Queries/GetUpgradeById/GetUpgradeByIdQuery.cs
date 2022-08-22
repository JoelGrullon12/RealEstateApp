using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.Upgrades;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Upgrades.Queries.GetUpgradeById
{
    /// <summary>
    /// Parametros para buscar una mejora por el Id
    /// </summary>
    public class GetUpgradeByIdQuery:IRequest<UpgradeResponse>
    {
        /// <example>
        /// 2
        /// </example>
        [SwaggerParameter(Description = "Id de la mejora")]
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
