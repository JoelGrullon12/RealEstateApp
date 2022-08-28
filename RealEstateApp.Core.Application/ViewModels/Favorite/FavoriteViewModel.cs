using RealEstateApp.Core.Application.ViewModels.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Favorite
{
    public class FavoriteViewModel:BaseViewModel
    {
        public string UserId { get; set; }
        public int PropertyId { get; set; }

        #region Navigation Properties
        public PropertyViewModel Property { get; set; }
        #endregion
    }
}
