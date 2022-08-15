using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Upgrades.Commands.DeleteUpgrade
{
    public class DeleteUpgradeCommand:IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteUpgradeCommandHandler : IRequestHandler<DeleteUpgradeCommand, bool>
    {
        private readonly IUpgradeRepository _upRepo;

        public DeleteUpgradeCommandHandler(IUpgradeRepository upRepo)
        {
            _upRepo = upRepo;
        }

        public async Task<bool> Handle(DeleteUpgradeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var upgrade = await _upRepo.GetByIdAsync(request.Id);

                if (upgrade == null)
                    return false;

                await _upRepo.DeleteAsync(upgrade);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
