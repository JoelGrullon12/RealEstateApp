using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Property
{
    public class SavePropertyViewModel : BaseViewModel
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
        public string ClientId { get; set; }
    }
}