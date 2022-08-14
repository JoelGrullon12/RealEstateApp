﻿using AutoMapper;
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

namespace RealEstateApp.Core.Application.Features.Agent.Queries.ListAgents
{
    public class ListAgentsQuery : IRequest<IList<AgentResponse>>
    { }

    public class ListAgentsQueryHandler : IRequestHandler<ListAgentsQuery, IList<AgentResponse>>
    {
        private readonly IUserService _userService;
        private readonly IPropertyRepository _propRepo;

        public ListAgentsQueryHandler(IUserService userService,IPropertyRepository propRepo)
        {
            _userService = userService;
            _propRepo = propRepo;
        }

        public async Task<IList<AgentResponse>> Handle(ListAgentsQuery request, CancellationToken cancellationToken)
        {
            var agents = await _userService.GetAllViewModel();

            List<AgentResponse> response = new();

            foreach(var agent in agents)
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

            return response;
        }
    }

}