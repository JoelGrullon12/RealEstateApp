using RealEstateApp.Core.Application.ViewModels.Property;

namespace RealEstateApp.Core.Application.ViewModels.Favorite
{
    public class FavoriteViewModel : BaseViewModel
    {
        public string UserId { get; set; }
        public int PropertyId { get; set; }

        #region Navigation Properties
        public PropertyViewModel Property { get; set; }
        #endregion
    }
}