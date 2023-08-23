using RealEstateApp.Core.Domain.Common;

namespace RealEstateApp.Core.Domain.Entities
{
    public class Favorite : AuditableBaseEntity
    {
        public string UserId { get; set; }
        public int PropertyId { get; set; }

        #region Navigation Properties
        public Property Property { get; set; }
        #endregion
    }
}