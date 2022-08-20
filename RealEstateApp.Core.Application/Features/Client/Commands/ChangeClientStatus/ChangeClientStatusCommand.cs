using MediatR;
using RealEstateApp.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Agent.Commands.ChangeClientStatus
{
    public class ChangeClientStatusCommand:IRequest<bool>
    {
        public string ClientId { get; set; }
        public bool Status { get; set; }
    }

    public class ChangeClientStatusCommandHandler : IRequestHandler<ChangeClientStatusCommand, bool>
    {
        private readonly IUserService _userService;

        public ChangeClientStatusCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(ChangeClientStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _userService.GetByIdViewModel(request.ClientId);

                if (client == null)
                    return false;

                client.IsActive = request.Status;
                await _userService.SetUserStatus(request.ClientId, request.Status);
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }

}
