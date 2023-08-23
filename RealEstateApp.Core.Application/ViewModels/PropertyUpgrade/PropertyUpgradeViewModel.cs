using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.Upgrade;

namespace RealEstateApp.Core.Application.ViewModels.PropertyUpgrade
{
    public class PropertyUpgradeViewModel:BaseViewModel
    {
        public int PropertyId { get; set; }
        public int UpgradeId { get; set; }

        #region Navigation Properties
        public PropertyViewModel Property { get; set; }
        public UpgradeViewModel Upgrade { get; set; }
        #endregion
    }
}