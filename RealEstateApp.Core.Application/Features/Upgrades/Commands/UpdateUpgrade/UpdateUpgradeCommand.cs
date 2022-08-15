using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.DTO.API.Upgrades;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Upgrades.Commands.UpdateUpgrade
{
    public class UpdateUpgradeCommand:IRequest<UpgradeResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateUpgradeCommandHandler : IRequestHandler<UpdateUpgradeCommand, UpgradeResponse>
    {
        private readonly IUpgradeRepository _upRepo;
        private readonly IMapper _mapper;

        public UpdateUpgradeCommandHandler(IUpgradeRepository upRepo, IMapper mapper)
        {
            _upRepo = upRepo;
            _mapper = mapper;
        }

        public async Task<UpgradeResponse> Handle(UpdateUpgradeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var upgrade = _mapper.Map<Upgrade>(request);

                if (upgrade == null)
                    return null;

                await _upRepo.UpdateAsync(upgrade, request.Id);
                return _mapper.Map<UpgradeResponse>(upgrade);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
