using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.Agents;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Agent.Queries.ListAgents
{
    /// <summary>
    /// Parametros para listar los agentes
    /// </summary>
    public class ListClientsQuery : IRequest<IList<AgentResponse>>
    { }

    public class ListAgentsQueryHandler : IRequestHandler<ListClientsQuery, IList<AgentResponse>>
    {
        private readonly IUserService _userService;
        private readonly IPropertyRepository _propRepo;

        public ListAgentsQueryHandler(IUserService userService,IPropertyRepository propRepo)
        {
            _userService = userService;
            _propRepo = propRepo;
        }

      
        public async Task<IList<AgentResponse>> Handle(ListClientsQuery request, CancellationToken cancellationToken)
        {
            var agents = await _userService.GetAllViewModel();

            List<AgentResponse> response = new();

            foreach(var agent in agents)
            {
                if (agent.Role == Roles.Agent.ToString())
                {
                    int propCount = (await _propRepo.GetAllAsync()).Where(prop => prop.AgentId == agent.Id).Count();
                    response.Add(new AgentResponse
                    {
                        Id = agent.Id,
                        Name = agent.FirstName,
                        LastName = agent.LastName,
                        Email = agent.Email,
                        Phone = agent.Phone,
                        PropertiesCount = propCount
                    });
                }
            }

            return response;
        }
    }

}
