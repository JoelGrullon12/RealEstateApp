using MediatR;
using RealEstateApp.Core.Application.DTO.API.Agents;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Agent.Queries.GetAgentById
{
    public class GetAgentByIdQuery:IRequest<AgentResponse>
    {
        public string Id { get; set; }
    }

    public class GetAgentByIdQueryHandler : IRequestHandler<GetAgentByIdQuery, AgentResponse>
    {
        private readonly IUserService _userService;
        private readonly IPropertyRepository _propRepo;

        public GetAgentByIdQueryHandler(IUserService userService, IPropertyRepository propRepo)
        {
            _userService = userService;
            _propRepo = propRepo;
        }

        public async Task<AgentResponse> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
        {
            var agent = await _userService.GetByIdSaveViewModel(request.Id);
            var props = await _propRepo.GetAllAsync();

            int propCount = (await _propRepo.GetAllAsync()).Where(prop => prop.AgentId == agent.Id).Count();

            var response = new AgentResponse
            {
                Id = agent.Id,
                Name = agent.FirstName,
                LastName = agent.LastName,
                Email = agent.Email,
                Phone = agent.Phone,
                PropertiesCount = propCount
            };

            return response;
        }
    }

}
