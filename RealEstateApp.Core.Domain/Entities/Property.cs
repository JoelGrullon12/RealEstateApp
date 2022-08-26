using RealEstateApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Domain.Entities
{
    public class Property : AuditableBaseEntity
    {
        public int Code { get; set; }
        public int TypeId { get; set; }
        // navigation propertly
        public int SellTypeId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Size { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
        public string ImageUrl4 { get; set; }
        public string AgentId { get; set; }
        public string ClientId { get; set; }

        #region Navigation Properties
        public PropertyType Type { get; set; }
        public SellType SellType { get; set; }
        public ICollection<PropertyImg> Images { get; set; }

        //Upgrades
        public ICollection<Upgrade> Upgrades { get; set; }
        public List<PropertyUpgrade> PropertyUpgrades { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        #endregion
    }
}
