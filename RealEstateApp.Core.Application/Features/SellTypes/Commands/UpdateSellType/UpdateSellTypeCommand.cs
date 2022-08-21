using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.DTO.API.SellTypes;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.SellTypes.Commands.UpdateSellType
{
    /// <summary>
    /// Parametros para actualizar un tipo de venta
    /// </summary>
    public class UpdateSellTypeCommand:IRequest<SellTypeResponse>
    {
        /// <example>
        /// 3
        /// </example>
        [SwaggerParameter(Description = "Id del tipo de venta")]
        public int Id { get; set; }

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

    public class UpdateSellTypeCommandHandler : IRequestHandler<UpdateSellTypeCommand, SellTypeResponse>
    {
        private readonly ISellTypeRepository _sellTypeRepo;
        private readonly IMapper _mapper;

        public UpdateSellTypeCommandHandler(ISellTypeRepository sellTypeRepo, IMapper mapper)
        {
            _sellTypeRepo = sellTypeRepo;
            _mapper = mapper;
        }

        public async Task<SellTypeResponse> Handle(UpdateSellTypeCommand request, CancellationToken cancellationToken)
        {
            var sellType = _mapper.Map<SellType>(request);

            if (sellType == null)
                return null;

            await _sellTypeRepo.UpdateAsync(sellType, request.Id);
            return _mapper.Map<SellTypeResponse>(sellType);
        }
    }

}
