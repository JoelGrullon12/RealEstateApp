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

namespace RealEstateApp.Core.Application.Features.SellTypes.Queries.ListSellType
{
    public class ListSellTypeQuery:IRequest<IList<SellTypeResponse>>
    {
    }

    public class ListSellTypeQueryHandler : IRequestHandler<ListSellTypeQuery, IList<SellTypeResponse>>
    {
        private readonly ISellTypeRepository _sellTypeRepo;
        private readonly IMapper _mapper;

        public ListSellTypeQueryHandler(ISellTypeRepository sellTypeRepo, IMapper mapper)
        {
            _sellTypeRepo = sellTypeRepo;
            _mapper = mapper;
        }

        public async Task<IList<SellTypeResponse>> Handle(ListSellTypeQuery request, CancellationToken cancellationToken)
        {
            var sellTypes = await _sellTypeRepo.GetAllAsync();
            return _mapper.Map<List<SellTypeResponse>>(sellTypes);
        }
    }

}
