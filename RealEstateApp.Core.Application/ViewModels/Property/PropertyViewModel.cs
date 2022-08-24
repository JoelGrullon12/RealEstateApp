using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.PropertyImg;
using RealEstateApp.Core.Application.ViewModels.PropertyType;
using RealEstateApp.Core.Application.ViewModels.SellType;
using RealEstateApp.Core.Application.ViewModels.Upgrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Property
{
    public class PropertyViewModel : BaseViewModel
    {
        public int Code { get; set; }
        public int TypeId { get; set; }
        public int SellTypeId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Size { get; set; }
        public string AgentId { get; set; }

        public DateTime Created { get; set; }

        #region Navigation Properties
        public PropertyTypeViewModel Type { get; set; }
        public SellTypeViewModel SellType { get; set; }

        public ICollection<PropertyImgViewModel> Images { get; set; }
        public ICollection<UpgradeViewModel> Upgrades { get; set; }
        public ICollection<FavoriteViewModel> Favorites { get; set; }
        #endregion
    }
}