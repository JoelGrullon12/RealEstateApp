﻿using MediatR;
using RealEstateApp.Core.Application.Dtos.API.Agents;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Agent.Queries.GetAgentById
{
    /// <summary>
    /// Parametros para buscar un agente por el Id
    /// </summary>
    public class GetClientByIdQuery:IRequest<AgentResponse>
    {
        /// <example>
        /// dfc25c57-e56a-482d-9ec5-a005dac64551
        /// </example>
        [SwaggerParameter(Description = "Id del Agente que se esta buscando")]
        public string Id { get; set; }
    }

    public class GetAgentByIdQueryHandler : IRequestHandler<GetClientByIdQuery, AgentResponse>
    {
        private readonly IUserService _userService;
        private readonly IPropertyRepository _propRepo;

        public GetAgentByIdQueryHandler(IUserService userService, IPropertyRepository propRepo)
        {
            _userService = userService;
            _propRepo = propRepo;
        }

        public async Task<AgentResponse> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var agent = await _userService.GetByIdSaveViewModel(request.Id);

            if (agent == null || agent.Role != Roles.Agent.ToString())
                return null;

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