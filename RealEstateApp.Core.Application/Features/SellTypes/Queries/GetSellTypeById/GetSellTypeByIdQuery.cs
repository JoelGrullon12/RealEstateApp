using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.DTO.API.SellTypes;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.SellTypes.Queries.GetSellTypeById
{
    public class GetSellTypeByIdQuery:IRequest<SellTypeResponse>
    {
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
