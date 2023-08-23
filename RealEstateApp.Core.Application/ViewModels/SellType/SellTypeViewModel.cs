using RealEstateApp.Core.Application.ViewModels.Property;
using System.Collections.Generic;

namespace RealEstateApp.Core.Application.ViewModels.SellType
{
    public class SellTypeViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        #region Navigation Properties
        public ICollection<PropertyViewModel> Properties { get; set; }
        #endregion
    }
}