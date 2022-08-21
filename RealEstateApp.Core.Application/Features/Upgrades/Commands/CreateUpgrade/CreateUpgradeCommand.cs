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

namespace RealEstateApp.Core.Application.Features.Upgrades.Commands.CreateUpgrade
{
    /// <summary>
    /// Parametros para crear una mejora
    /// </summary>
    public class CreateUpgradeCommand:IRequest<bool>
    {
        /// <example>
        /// Terraza
        /// </example>
        [SwaggerParameter(Description = "Nombre de la mejora")]
        public string Name { get; set; }

        /// <example>
        /// Espacio en el techo de la propiedad para recreo
        /// </example>
        [SwaggerParameter(Description = "Descripcion de la mejora")]
        public string Description { get; set; }
    }

    public class CreateUpgradeCommandHandler : IRequestHandler<CreateUpgradeCommand, bool>
    {
        private readonly IUpgradeRepository _upRepo;
        private readonly IMapper _mapper;

        public CreateUpgradeCommandHandler(IUpgradeRepository upRepo, IMapper mapper)
        {
            _upRepo = upRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateUpgradeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var upgrade = _mapper.Map<Upgrade>(request);
                upgrade = await _upRepo.AddAsync(upgrade);
                return upgrade != null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
