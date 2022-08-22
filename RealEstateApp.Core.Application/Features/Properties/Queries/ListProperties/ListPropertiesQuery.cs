using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.Properties;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Properties.Queries.ListProperties
{
    /// <summary>
    /// Parametros para listar todas las propiedades
    /// </summary>
    public class ListPropertiesQuery : IRequest<IList<PropertyResponse>>
    {
    }

    public class ListPropertiesQueryHandler : IRequestHandler<ListPropertiesQuery, IList<PropertyResponse>>
    {
        private readonly IPropertyRepository _propRepo;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ListPropertiesQueryHandler(IPropertyRepository propRepo, IMapper mapper, IUserService userService)
        {
            _propRepo = propRepo;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IList<PropertyResponse>> Handle(ListPropertiesQuery request, CancellationToken cancellationToken)
        {
            var props = await _propRepo.GetAllWithIncludes(new List<string> { "Type", "SellType", "Upgrades" });

            List<PropertyResponse> response = new();

            foreach(var prop in props)
            {
                PropertyResponse property = _mapper.Map<PropertyResponse>(prop);
                var agent=await _userService.GetByIdViewModel(prop.AgentId);

                int propCount = 0;

                foreach(var propertyByAgent in props)
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

            return response;
        }
    }
}
