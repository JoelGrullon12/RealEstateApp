using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.PropertyTypes;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.PropertyTypes.Commands.UpdatePropertyType
{
    /// <summary>
    /// Parametros para editar un tipo de propiedad
    /// </summary>
    public class UpdatePropertyTypeCommand:IRequest<PropertyTypeResponse>
    {
        /// <example>
        /// 3
        /// </example>
        [SwaggerParameter(Description = "Id del tipo de propiedad")]
        public int Id { get; set; }

        /// <example>
        /// Cabaña
        /// </example>
        [SwaggerParameter(Description = "Nombre del tipo de propiedad")]
        public string Name { get; set; }

        /// <example>
        /// Edificio de madera en medio del bosque
        /// </example>
        [SwaggerParameter(Description = "Descripcion del tipo de propiedad")]
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
