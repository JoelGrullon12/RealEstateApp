﻿using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.DTO.API.PropertyTypes;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.PropertyTypes.Queries.GetPropertyTypeById
{
    public class GetPropertyTypeByIdQuery:IRequest<PropertyTypeResponse>
    {
        public int Id { get; set; }
    }

    public class GetPropertyTypeByIdQueryHandler : IRequestHandler<GetPropertyTypeByIdQuery, PropertyTypeResponse>
    {
        private readonly IPropertyTypeRepository _propTypeRepo;
        private readonly IMapper _mapper;

        public GetPropertyTypeByIdQueryHandler(IPropertyTypeRepository propTypeRepo, IMapper mapper)
        {
            _propTypeRepo = propTypeRepo;
            _mapper = mapper;
        }

        public async Task<PropertyTypeResponse> Handle(GetPropertyTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var propType = await _propTypeRepo.GetByIdAsync(request.Id);
            return _mapper.Map<PropertyTypeResponse>(propType);
        }
    }

}
