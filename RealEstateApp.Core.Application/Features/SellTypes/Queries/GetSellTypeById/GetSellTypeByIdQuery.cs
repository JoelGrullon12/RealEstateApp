using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.DTO.API.SellTypes;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.SellTypes.Queries.GetSellTypeById
{
    /// <summary>
    /// Parametros para buscar un tipo de venta por su Id
    /// </summary>
    public class GetSellTypeByIdQuery:IRequest<SellTypeResponse>
    {
        /// <example>
        /// 3
        /// </example>
        [SwaggerParameter(Description = "Id del tipo de venta")]
        public int Id { get; set; }
    }

    public class GetSellTypeByIdQueryHandler : IRequestHandler<GetSellTypeByIdQuery, SellTypeResponse>
    {
        private readonly ISellTypeRepository _sellTypeRepo;
        private readonly IMapper _mapper;

        public GetSellTypeByIdQueryHandler(ISellTypeRepository sellTypeRepo, IMapper mapper)
        {
            _sellTypeRepo = sellTypeRepo;
            _mapper = mapper;
        }

        public async Task<SellTypeResponse> Handle(GetSellTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var sellType = await _sellTypeRepo.GetByIdAsync(request.Id);
            return _mapper.Map<SellTypeResponse>(sellType);
        }
    }

}
