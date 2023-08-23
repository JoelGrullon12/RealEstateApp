using System.ComponentModel.DataAnnotations;

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