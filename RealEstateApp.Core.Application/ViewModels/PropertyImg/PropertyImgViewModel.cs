using RealEstateApp.Core.Application.ViewModels.Property;

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