using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.PropertyType
{
    public class SavePropertyTypeViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Debes colocar el nombre del tipo de propiedad")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debes colocar la descripción del tipo de propiedad")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
