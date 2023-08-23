using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.API.Properties;
using RealEstateApp.Core.Application.Features.Properties.Queries.GetPropertyByCode;
using RealEstateApp.Core.Application.Features.Properties.Queries.GetPropertyById;
using RealEstateApp.Core.Application.Features.Properties.Queries.ListProperties;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace RealEstateApp.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin,Developer")]
    [SwaggerTag("Listar y consultar datos de las propiedades")]
    public class PropertyController : BaseApiController
    {

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listado de propiedades",
            Description = "Lista todas las propiedades mostrando el agente que ha creado a cada una"
            )]
        public async Task<IActionResult> Get()
        {
            try
            {
                var propList = await Mediator.Send(new ListPropertiesQuery());

                if (propList == null || propList.Count == 0)
                    return NotFound();

                return Ok(propList);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Propiedad por Id",
            Description = "Obtiene los datos de la propiedad correspondiente al Id suministrado"
            )]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var prop = await Mediator.Send(new GetPropertyByIdQuery
                {
                    Id = id
                });

                if (prop == null)
                    return NotFound();

                return Ok(prop);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet("GetByCode/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Propiedad por Codigo",
            Description = "Obtiene los datos de la propiedad correspondiente al Codigo de 6 digitos suministrado"
            )]
        public async Task<IActionResult> GetByCode([FromRoute] int code)
        {
            try
            {
                var prop = await Mediator.Send(new GetPropertyByCodeQuery
                {
                    Code = code
                });

                if (prop == null)
                    return NotFound();

                return Ok(prop);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
