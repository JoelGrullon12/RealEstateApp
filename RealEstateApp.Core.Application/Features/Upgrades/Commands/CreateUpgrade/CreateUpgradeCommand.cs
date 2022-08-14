using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Upgrades.Commands.CreateUpgrade
{
    public class CreateUpgradeCommand:IRequest<bool>
    {
        public string Name { get; set; }
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
