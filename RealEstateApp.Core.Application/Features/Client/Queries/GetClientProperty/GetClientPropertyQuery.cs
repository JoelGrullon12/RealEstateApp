using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.DTO.API.Properties;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Client.Queries.GetClientProperty
{
    public class GetClientPropertyQuery:IRequest<IList<PropertyResponse>>
    {
        public string ClientId { get; set; }
    }

    public class GetClientPropertyQueryHandler : IRequestHandler<GetClientPropertyQuery, IList<PropertyResponse>>
    {
        private readonly IPropertyRepository _propRepo;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetClientPropertyQueryHandler(IPropertyRepository propRepo, IUserService userService, IMapper mapper)
        {
            _propRepo = propRepo;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IList<PropertyResponse>> Handle(GetClientPropertyQuery request, CancellationToken cancellationToken)
        {
            var props = await _propRepo.GetAllWithIncludes(new List<string> { "Type", "SellType", "Upgrades" });

            List<PropertyResponse> response = new();

            foreach (var prop in props)
            {
                if (prop.ClientId == request.ClientId)
                {
                    var property = _mapper.Map<PropertyResponse>(prop);
                    var client = await _userService.GetByIdViewModel(prop.ClientId);

                    int propCount = 0;

                    foreach (var propertyByClient in props)
                    {
                        if (propertyByClient.ClientId == client.Id)
                            propCount++;
                    }


                    property.Client = new()
                    {
                        Id = client.Id,
                        Name = client.FirstName,
                        LastName = client.LastName,
                        Email = client.Email,
                        Phone = client.Phone,
                        PropertiesCount = propCount
                    };

                    response.Add(property);
                }
            }

            return response;
        }
    }

}
