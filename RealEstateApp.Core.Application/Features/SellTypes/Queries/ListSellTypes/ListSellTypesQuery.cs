using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.SellTypes;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.SellTypes.Queries.ListSellTypes
{
    /// <summary>
    /// Parametros para listar todos los tipos de venta
    /// </summary>
    public class ListSellTypesQuery:IRequest<IList<SellTypeResponse>>
    {
    }

    public class ListSellTypesQueryHandler : IRequestHandler<ListSellTypesQuery, IList<SellTypeResponse>>
    {
        private readonly ISellTypeRepository _sellTypeRepo;
        private readonly IMapper _mapper;

        public ListSellTypesQueryHandler(ISellTypeRepository sellTypeRepo, IMapper mapper)
        {
            _sellTypeRepo = sellTypeRepo;
            _mapper = mapper;
        }

        public async Task<IList<SellTypeResponse>> Handle(ListSellTypesQuery request, CancellationToken cancellationToken)
        {
            var sellTypes = await _sellTypeRepo.GetAllAsync();
            return _mapper.Map<List<SellTypeResponse>>(sellTypes);
        }
    }

}
