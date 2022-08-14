﻿using AutoMapper;
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

namespace RealEstateApp.Core.Application.Features.Properties.Queries.GetPropertyByCode
{
    public class GetPropertyByIdQuery : IRequest<PropertyResponse>
    {
        public int Id { get; set; }
    }

    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyResponse>
    {
        private readonly IPropertyRepository _propRepo;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetPropertyByIdQueryHandler(IPropertyRepository propRepo, IUserService userService, IMapper mapper)
        {
            _propRepo = propRepo;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<PropertyResponse> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var prop = await _propRepo.GetByIdWithIncludes(request.Id, new List<string> { "Type", "SellType" }, new List<string> { "Upgrades" });
            var agent = await _userService.GetByIdSaveViewModel(prop.AgentId);

            var response = _mapper.Map<PropertyResponse>(prop);
            int propCount = 0;

            var props = await _propRepo.GetAllAsync();
            foreach (var propertyByAgent in props)
            {
                if (propertyByAgent.AgentId == agent.Id)
                    propCount++;
            }

            response.Agent = new()
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
