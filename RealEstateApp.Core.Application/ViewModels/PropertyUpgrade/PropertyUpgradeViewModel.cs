using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.Upgrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
