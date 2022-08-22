using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.Clients;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Agent.Queries.ListClients
{
    public class ListClientsQuery : IRequest<IList<ClientResponse>>
    { }

    public class ListClientsQueryHandler : IRequestHandler<ListClientsQuery, IList<ClientResponse>>
    {
        private readonly IUserService _userService;
        private readonly IPropertyRepository _propRepo;

        public ListClientsQueryHandler(IUserService userService,IPropertyRepository propRepo)
        {
            _userService = userService;
            _propRepo = propRepo;
        }

        public async Task<IList<ClientResponse>> Handle(ListClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _userService.GetAllViewModel();

            List<ClientResponse> response = new();

            foreach(var client in clients)
            {
                int propCount = (await _propRepo.GetAllAsync()).Where(prop => prop.ClientId == client.Id).Count();
                response.Add(new ClientResponse
                {
                    Id = client.Id,
                    Name = client.FirstName,
                    LastName = client.LastName,
                    Email = client.Email,
                    Phone = client.Phone,
                    PropertiesCount = propCount
                });
            }

            return response;
        }
    }

}
