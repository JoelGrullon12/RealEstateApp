using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.PropertyTypes.Commands.DeletePropertyType
{
    public class DeletePropertyTypeCommand:IRequest<bool>
    {
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
                await _propTypeRepo.DeleteAsync(request.Id);
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }

}
