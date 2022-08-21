using MediatR;
using RealEstateApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Agent.Commands.ChangeAgentStatus
{
    /// <summary>
    /// Parametros para cambiar el estado de un agente
    /// </summary>
    public class ChangeClientStatusCommand:IRequest<bool>
    {
        /// <example>
        /// dfc25c57-e56a-482d-9ec5-a005dac64551
        /// </example>
        [SwaggerParameter(Description ="Id del agente que se va a cambiar el estado")]
        public string AgentId { get; set; }

        /// <example>
        /// true
        /// </example>
        [SwaggerParameter(Description = "Nuevo estado del agente en formato booleano")]
        public bool Status { get; set; }
    }

    public class ChangeAgentStatusCommandHandler : IRequestHandler<ChangeClientStatusCommand, bool>
    {
        private readonly IUserService _userService;

        public ChangeAgentStatusCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(ChangeClientStatusCommand request, CancellationToken cancellationToken)
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
