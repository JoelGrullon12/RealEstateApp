using MediatR;
using RealEstateApp.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Agent.Commands.ChangeAgentStatus
{
    public class ChangeAgentStatusCommand:IRequest<bool>
    {
        public string AgentId { get; set; }
        public bool Status { get; set; }
    }

    public class ChangeAgentStatusCommandHandler : IRequestHandler<ChangeAgentStatusCommand, bool>
    {
        private readonly IUserService _userService;

        public ChangeAgentStatusCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(ChangeAgentStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var agent = await _userService.GetByIdViewModel(request.AgentId);

                if (agent == null)
                    return false;

                agent.IsActive = request.Status;
                await _userService.SetUserStatus(request.AgentId, request.Status);
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }

}
