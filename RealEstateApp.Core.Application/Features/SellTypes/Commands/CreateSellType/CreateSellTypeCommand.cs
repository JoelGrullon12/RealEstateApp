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

namespace RealEstateApp.Core.Application.Features.SellTypes.Commands.CreateSellType
{
    public class CreateSellTypeCommand : IRequest<bool>
    {
        public string Name { get; set; }
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
