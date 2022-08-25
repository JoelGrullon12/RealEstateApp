using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.Upgrade
{
    public class SaveUpgradeViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Debes colocar el nombre de la mejora")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debes colocar la descripción de la mejora")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}