﻿using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.PropertyTypes.Commands.DeletePropertyType
{
    /// <summary>
    /// Parametros para eliminar un tipo de propiedad
    /// </summary>
    public class DeletePropertyTypeCommand:IRequest<bool>
    {
        /// <example>
        /// 3
        /// </example>
        [SwaggerParameter(Description = "Id del tipo de propiedad")]
        public int Id { get; set; }
    }

    public class DeletePropertyTypeCommandHandler : IRequestHandler<DeletePropertyTypeCommand, bool>
    {
        private readonly IPropertyTypeRepository _propTypeRepo;

        public DeletePropertyTypeCommandHandler(IPropertyTypeRepository propTypeRepo)
        {
            _propTypeRepo = propTypeRepo;
        }

        public async Task<bool> Handle(DeletePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var propType = await _propTypeRepo.GetByIdAsync(request.Id);

                if (propType == null)
                    return false;

                await _propTypeRepo.DeleteAsync(propType);
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }

}
