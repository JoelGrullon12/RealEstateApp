using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.Property
{
    public class SavePropertyViewModel : BaseViewModel
    {
        public int Code { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar el tipo de propiedad")]
        public int TypeId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar el tipo de venta")]
        public int SellTypeId { get; set; }
        public double Price { get; set; }

        [Required(ErrorMessage = "Debes colocar la descripción")]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes colocar la cantidad de habitaciones")]
        public int Bedrooms { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Debes colocar la cantidad de baños")]
        public int Bathrooms { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes colocar el tamaño en metros")]
        public int Size { get; set; }
        public string AgentId { get; set; }
        public string ClientId { get; set; }

        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
        public string ImageUrl4 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile ImageFile1 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile ImageFile2 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile ImageFile3 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile ImageFile4 { get; set; }

        public string Upgrades { get; set; }
    }
}