using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.DTO.API.SellTypes;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.SellTypes.Commands.UpdateSellType
{
    public class UpdateSellTypeCommand:IRequest<SellTypeResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
            await _sellTypeRepo.UpdateAsync(sellType, request.Id);
            return _mapper.Map<SellTypeResponse>(sellType);
        }
    }

}
