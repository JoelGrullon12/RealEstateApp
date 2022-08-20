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

namespace RealEstateApp.Core.Application.Features.Agent.Queries.GetAgentProperty
{
    public class GetAgentPropertyQuery:IRequest<IList<PropertyResponse>>
    {
        public string AgentId { get; set; }
    }

    public class GetAgentPropertyQueryHandler : IRequestHandler<GetAgentPropertyQuery, IList<PropertyResponse>>
    {
        private readonly IPropertyRepository _propRepo;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAgentPropertyQueryHandler(IPropertyRepository propRepo, IUserService userService, IMapper mapper)
        {
            _propRepo = propRepo;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IList<PropertyResponse>> Handle(GetAgentPropertyQuery request, CancellationToken cancellationToken)
        {
            var props = await _propRepo.GetAllWithIncludes(new List<string> { "Type", "SellType", "Upgrades" });

            List<PropertyResponse> response = new();

            foreach (var prop in props)
            {
                if (prop.AgentId == request.AgentId)
                {
                    var property = _mapper.Map<PropertyResponse>(prop);
                    var agent = await _userService.GetByIdViewModel(prop.AgentId);

                    int propCount = 0;

                    foreach (var propertyByAgent in props)
                    {
                        if (propertyByAgent.AgentId == agent.Id)
                            propCount++;
                    }


                    property.Agent = new()
                    {
                        Id = agent.Id,
                        Name = agent.FirstName,
                        LastName = agent.LastName,
                        Email = agent.Email,
                        Phone = agent.Phone,
                        PropertiesCount = propCount
                    };

                    response.Add(property);
                }
            }

            return response;
        }
    }

}
