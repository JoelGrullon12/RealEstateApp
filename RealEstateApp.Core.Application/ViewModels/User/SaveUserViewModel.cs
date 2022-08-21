using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debes colocar tu nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debes colocar tu apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debes colocar tu correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debes colocar tu telefono")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Debes colocar tu cedula")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "Debes colocar tu nombre de usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debes colocar tu contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debes colocar tu contraseña")]
        [Compare(nameof(Password), ErrorMessage = "Ambas contraseñas deben coincidir")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debes colocar el tipo de usuario")]
        public string Type { get; set; }
    }
}