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

namespace RealEstateApp.Core.Application.Features.SellTypes.Commands.CreateSellType
{
    /// <summary>
    /// Parametros para crear un tipo de venta
    /// </summary>
    public class CreateSellTypeCommand : IRequest<bool>
    {
        /// <example>
        /// Directa
        /// </example>
        [SwaggerParameter(Description = "Nombre del tipo de venta")]
        public string Name { get; set; }

        /// <example>
        /// Venta en persona con el dueño y el comprador presente
        /// </example>
        [SwaggerParameter(Description = "Descripcion del tipo de venta")]
        public string Description { get; set; }
    }

    public class CreateSellTypeCommandHandler : IRequestHandler<CreateSellTypeCommand, bool>
    {
        private readonly ISellTypeRepository _sellTypeRepo;
        private readonly IMapper _mapper;

        public CreateSellTypeCommandHandler(ISellTypeRepository sellTypeRepo, IMapper mapper)
        {
            _sellTypeRepo = sellTypeRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateSellTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sellType = _mapper.Map<SellType>(request);
                sellType = await _sellTypeRepo.AddAsync(sellType);
                return sellType != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
