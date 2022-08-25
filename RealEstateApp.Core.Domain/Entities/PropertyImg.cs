using RealEstateApp.Core.Domain.Common;

namespace RealEstateApp.Core.Domain.Entities
{
    public class PropertyImg : AuditableBaseEntity
    {
        public int PropertyId { get; set; }
        public string ImgUrl { get; set; }

        #region Navigation Properties
        public Property Property { get; set; }
        #endregion
    }
}