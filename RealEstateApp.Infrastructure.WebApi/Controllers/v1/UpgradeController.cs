using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.API.Upgrades;
using RealEstateApp.Core.Application.Features.Upgrades.Commands.CreateUpgrade;
using RealEstateApp.Core.Application.Features.Upgrades.Commands.DeleteUpgrade;
using RealEstateApp.Core.Application.Features.Upgrades.Commands.UpdateUpgrade;
using RealEstateApp.Core.Application.Features.Upgrades.Queries.GetUpgradeById;
using RealEstateApp.Core.Application.Features.Upgrades.Queries.ListUpgrades;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApi.Controllers.v1
{
    [Authorize(Roles = "Admin,Developer")]
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Mejoras")]
    public class UpgradeController : BaseApiController
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Crear Mejora",
            Description = "Crea una mejora con los datos suministrados en formato JSON"
            )]
        public async Task<IActionResult> Post([FromBody] CreateUpgradeCommand request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var wasCreated = await Mediator.Send(request);

                if (wasCreated)
                    return NoContent();

                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error creando la mejora");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpgradeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Editar Mejora",
            Description = "Edita una mejora segun los datos suministrados en formato JSON y modifica el registro correspondiente al Id"
            )]
        public async Task<IActionResult> Put([FromBody] UpdateUpgradeCommand request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var response = await Mediator.Send(request);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpgradeResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar Mejoras",
            Description = "Lista todas las mejoras guardadas en la base de datos"
            )]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await Mediator.Send(new ListUpgradesQuery());

                if (response == null || response.Count == 0)
                    return NotFound();

                return Ok(response);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpgradeResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Mejora por Id",
            Description = "Muestra los datos de la mejora correspondiente al Id suministrado"
            )]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var response = await Mediator.Send(new GetUpgradeByIdQuery
                {
                    Id = id
                });

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Eliminar Mejora",
            Description = "Elimina la Mejora correspondiente al Id suministrado"
            )]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var response = await Mediator.Send(new DeleteUpgradeCommand
                {
                    Id = id
                });

                if (response)
                    return NoContent();

                return NotFound($"No existe una mejora con el Id '{id}'");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}