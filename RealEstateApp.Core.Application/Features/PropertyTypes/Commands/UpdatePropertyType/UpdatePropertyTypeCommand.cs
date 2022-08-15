using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.DTO.API.PropertyTypes;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.PropertyTypes.Commands.UpdatePropertyType
{
    public class UpdatePropertyTypeCommand:IRequest<PropertyTypeResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePropertyTypeCommandHandler : IRequestHandler<UpdatePropertyTypeCommand, PropertyTypeResponse>
    {
        private readonly IPropertyTypeRepository _propTypeRepo;
        private readonly IMapper _mapper;

        public UpdatePropertyTypeCommandHandler(IPropertyTypeRepository propTypeRepo, IMapper mapper)
        {
            _propTypeRepo = propTypeRepo;
            _mapper = mapper;
        }

        public async Task<PropertyTypeResponse> Handle(UpdatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            var propType = _mapper.Map<PropertyType>(request);

            if(propType == null)
                return null;

            await _propTypeRepo.UpdateAsync(propType, request.Id);

            return _mapper.Map<PropertyTypeResponse>(propType);
        }
    }

}
