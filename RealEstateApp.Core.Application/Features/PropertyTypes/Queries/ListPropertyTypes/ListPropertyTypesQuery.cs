using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.DTO.API.PropertyTypes;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.PropertyTypes.Queries.ListPropertyTypes
{
    /// <summary>
    /// Parametros para listar todos los tipos de propiedades
    /// </summary>
    public class ListPropertyTypesQuery:IRequest<IList<PropertyTypeResponse>>
    {
    }

    public class ListPropertyTypesQueryHandler : IRequestHandler<ListPropertyTypesQuery, IList<PropertyTypeResponse>>
    {
        private readonly IPropertyTypeRepository _propTypeRepo;
        private readonly IMapper _mapper;

        public ListPropertyTypesQueryHandler(IPropertyTypeRepository propTypeRepo, IMapper mapper)
        {
            _propTypeRepo = propTypeRepo;
            _mapper = mapper;
        }

        public async Task<IList<PropertyTypeResponse>> Handle(ListPropertyTypesQuery request, CancellationToken cancellationToken)
        {
            var propTypes = await _propTypeRepo.GetAllAsync();
            return _mapper.Map<List<PropertyTypeResponse>>(propTypes);
        }
    }

}
