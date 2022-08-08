using RealEstateApp.Core.Application.ViewModels.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.PropertyImg
{
    public class PropertyImgViewModel:BaseViewModel
    {
        public int PropertyId { get; set; }
        public string ImgUrl { get; set; }

        #region Navigation Properties
        public PropertyViewModel Property { get; set; }
        #endregion
    }
}
