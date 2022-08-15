using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.SellTypes.Commands.DeleteSellType
{
    public class DeleteSellTypeCommand:IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteSellTypeCommandHandler : IRequestHandler<DeleteSellTypeCommand, bool>
    {
        private readonly ISellTypeRepository _sellTypeRepo;

        public DeleteSellTypeCommandHandler(ISellTypeRepository sellTypeRepo)
        {
            _sellTypeRepo = sellTypeRepo;
        }

        public async Task<bool> Handle(DeleteSellTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sellType = await _sellTypeRepo.GetByIdAsync(request.Id);

                if (sellType == null)
                    return false;

                await _sellTypeRepo.DeleteAsync(sellType);
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }

}
