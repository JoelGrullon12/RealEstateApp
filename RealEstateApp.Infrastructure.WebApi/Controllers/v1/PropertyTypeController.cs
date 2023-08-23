using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.API.PropertyTypes;
using RealEstateApp.Core.Application.Features.PropertyTypes.Commands.CreatePropertyType;
using RealEstateApp.Core.Application.Features.PropertyTypes.Commands.DeletePropertyType;
using RealEstateApp.Core.Application.Features.PropertyTypes.Commands.UpdatePropertyType;
using RealEstateApp.Core.Application.Features.PropertyTypes.Queries.GetPropertyTypeById;
using RealEstateApp.Core.Application.Features.PropertyTypes.Queries.ListPropertyTypes;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin,Developer")]
    [SwaggerTag("Mantenimiento de Tipos de Propiedades")]
    public class PropertyTypeController : BaseApiController
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
            Summary = "Crear Tipo de Propiedad",
            Description = "Crea un tipo de propiedad con los datos suministrados en formato JSON"
            )]
        public async Task<IActionResult> Post([FromBody] CreatePropertyTypeCommand request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var wasCreated = await Mediator.Send(request);

                if (wasCreated)
                    return NoContent();

                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error creando el tipo de propiedad");
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyTypeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Editar Tipo de Propiedad",
            Description = "Edita un tipo de propiedad segun los datos suministrados en formato JSON y modifica el registro correspondiente al Id"
            )]
        public async Task<IActionResult> Put([FromBody] UpdatePropertyTypeCommand request)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyTypeResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listar Tipos de Propiedades",
            Description = "Lista todos los tipos de propiedades guardados en la base de datos"
            )]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await Mediator.Send(new ListPropertyTypesQuery());

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyTypeResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Tipo de Propiedad por Id",
            Description = "Muestra los datos del tipo de propiedad correspondiente al Id suministrado"
            )]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var response = await Mediator.Send(new GetPropertyTypeByIdQuery
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
            Summary = "Eliminar Tipo de Propiedad",
            Description = "Elimina el Tipo de Propiedad correspondiente al Id suministrado"
            )]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var response = await Mediator.Send(new DeletePropertyTypeCommand
                {
                    Id = id
                });

                if (response)
                    return NoContent();

                return NotFound($"No existe un tipo de propiedad con el Id '{id}'");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
