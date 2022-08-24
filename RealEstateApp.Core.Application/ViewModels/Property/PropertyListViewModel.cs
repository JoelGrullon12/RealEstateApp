using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Application.ViewModels.PropertyType;
using RealEstateApp.Core.Application.ViewModels.SellType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Property
{
    public class PropertyListViewModel
    {
       //public UserViewModel User { get; set; }
    
 
        public List<PropertyTypeViewModel> PropertyTypes { get; set; }
        public List<SellTypeViewModel> SellTypes { get; set; }


    }
}





