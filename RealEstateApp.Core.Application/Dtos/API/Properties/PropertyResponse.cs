using RealEstateApp.Core.Application.Dtos.API.PropertyTypes;
using RealEstateApp.Core.Application.Dtos.API.SellTypes;
using RealEstateApp.Core.Application.Dtos.API.Agents;
using RealEstateApp.Core.Application.Dtos.API.Upgrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateApp.Core.Application.Dtos.API.Clients;

namespace RealEstateApp.Core.Application.Dtos.API.Properties
{
    public class PropertyResponse
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Size { get; set; }

        public AgentResponse Agent { get; set; }
        public ClientResponse Client { get; set; }
        public PropertyTypeResponse Type { get; set; }
        public SellTypeResponse SellType { get; set; }

        //Upgrades
        public ICollection<UpgradeResponse> Upgrades { get; set; }
    }
}
