using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.PropertyTypes.Commands.CreatePropertyType
{
    /// <summary>
    /// Parametros para crear un tipo de propiedad
    /// </summary>
    public class CreatePropertyTypeCommand:IRequest<bool>
    {
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

    public class CreatePropertyTypeCommandHandler : IRequestHandler<CreatePropertyTypeCommand, bool>
    {
        private readonly IPropertyTypeRepository _propTypeRepo;
        private readonly IMapper _mapper;

        public CreatePropertyTypeCommandHandler(IPropertyTypeRepository propTypeRepo, IMapper mapper)
        {
            _propTypeRepo = propTypeRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            var propType = _mapper.Map<PropertyType>(request);
            propType = await _propTypeRepo.AddAsync(propType);

            return propType != null;
        }
    }

}
