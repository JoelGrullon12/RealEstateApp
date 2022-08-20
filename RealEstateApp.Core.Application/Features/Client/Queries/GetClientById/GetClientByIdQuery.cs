using MediatR;
using RealEstateApp.Core.Application.DTO.API.Clients;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Agent.Queries.GetClientById
{
    public class GetClientByIdQuery:IRequest<ClientResponse>
    {
        public string Id { get; set; }
    }

    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientResponse>
    {
        private readonly IUserService _userService;
        private readonly IPropertyRepository _propRepo;

        public GetClientByIdQueryHandler(IUserService userService, IPropertyRepository propRepo)
        {
            _userService = userService;
            _propRepo = propRepo;
        }

        public async Task<ClientResponse> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _userService.GetByIdViewModel(request.Id);

            if (client == null)
                return null;

            var props = await _propRepo.GetAllAsync();

            int propCount = (await _propRepo.GetAllAsync()).Where(prop => prop.ClientId == client.Id).Count();

            var response = new ClientResponse
            {
                Id = client.Id,
                Name = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Phone = client.Phone,
                PropertiesCount = propCount
            };

            return response;
        }
    }

}
