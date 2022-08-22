using RealEstateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Property
{
    public class PropertyListViewModel
    {
        public UserViewModel User { get; set; }
        public List<PropertyViewModel> PropertyTypes { get; set; }
        public List<PropertyViewModel> SellTypes { get; set; }

    }
}





