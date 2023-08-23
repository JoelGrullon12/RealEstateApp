using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.SellType
{
    public class SaveSellTypeViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Debes colocar el nombre del tipo de venta")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debes colocar la descripción del tipo de venta")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}