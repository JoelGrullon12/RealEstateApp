using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debes colocar el nombre de usuario o correo")]
        [DataType(DataType.Text)]
        public string UserOrEmail { get; set; }

        [Required(ErrorMessage = "Debes colocar la contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}