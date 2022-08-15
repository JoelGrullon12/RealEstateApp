using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.DTO.API.Agents;
using RealEstateApp.Core.Application.DTO.API.Properties;
using RealEstateApp.Core.Application.Features.Agent.Commands.ChangeAgentStatus;
using RealEstateApp.Core.Application.Features.Agent.Queries.GetAgentById;
using RealEstateApp.Core.Application.Features.Agent.Queries.GetAgentProperty;
using RealEstateApp.Core.Application.Features.Agent.Queries.ListAgents;
using System;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AgentController : BaseApiController
    {

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var agentList = await Mediator.Send(new ListAgentsQuery());

                if (agentList == null || agentList.Count == 0)
                    return NotFound();

                return Ok(agentList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            try
            {
                var agent = await Mediator.Send(new GetAgentByIdQuery
                {
                    Id = id
                });

                if (agent == null)
                    return NotFound();

                return Ok(agent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet("GetAgentProperties/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProperties([FromRoute] string id)
        {
            try
            {
                var props = await Mediator.Send(new GetAgentPropertyQuery
                {
                    AgentId = id
                });

                if (props == null || props.Count == 0)
                    return NotFound();

                return Ok(props);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPut("ChangeStatus")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeAgentStatusCommand request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var changed = await Mediator.Send(request);

                if (changed)
                    return NoContent();

                return NotFound($"No existe un agente con el Id '{request.AgentId}'");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
