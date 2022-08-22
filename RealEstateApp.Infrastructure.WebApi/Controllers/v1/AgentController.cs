using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.API.Agents;
using RealEstateApp.Core.Application.Dtos.API.Properties;
using RealEstateApp.Core.Application.Features.Agent.Commands.ChangeAgentStatus;
using RealEstateApp.Core.Application.Features.Agent.Queries.GetAgentById;
using RealEstateApp.Core.Application.Features.Agent.Queries.GetAgentProperty;
using RealEstateApp.Core.Application.Features.Agent.Queries.ListAgents;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles="Admin,Developer")]
    [SwaggerTag("Listar y activar/desactivar Agentes")]
    public class AgentController : BaseApiController
    {

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary ="Listado de agentes",
            Description ="Lista todos los agentes mostrando la cantidad de propiedades que ha creado cada agente"
            )]
        public async Task<IActionResult> Get()
        {
            try
            {
                var agentList = await Mediator.Send(new ListClientsQuery());

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
        [SwaggerOperation(
            Summary = "Agente por Id",
            Description = "Obtiene los datos del agente correspondiente al Id suministrado"
            )]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            try
            {
                var agent = await Mediator.Send(new GetClientByIdQuery
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
        [SwaggerOperation(
            Summary = "Propiedades de un agente",
            Description = "Obtiene los datos de las propiedades creadas por el agente correspondiente al Id suministrado"
            )]
        public async Task<IActionResult> GetProperties([FromRoute] string id)
        {
            try
            {
                var props = await Mediator.Send(new GetClientPropertyQuery
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
        [Authorize(Roles="Admin")]
        [HttpPut("ChangeStatus")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Cambiar estado de un agente",
            Description = "Cambia el estado del agente correspondiente al Id suministrado a activo o inactivo segun se indique"
            )]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeClientStatusCommand request)
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
