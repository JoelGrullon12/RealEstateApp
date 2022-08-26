using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debes colocar tu nombre")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debes colocar tu apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debes colocar tu correo")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debes colocar tu telefono")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Debes colocar tu cedula")]
        [DataType(DataType.Text)]
        public string DNI { get; set; }

        [Required(ErrorMessage = "Debes colocar tu nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debes colocar tu contraseña actual")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Debes colocar tu contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debes confirmar tu contraseña")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Ambas contraseñas deben coincidir")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debes colocar el tipo de usuario")]
        [DataType(DataType.Text)]
        public string Role { get; set; }
        public string ImgUrl { get; set; }
        public bool IsActive { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }
    }
}