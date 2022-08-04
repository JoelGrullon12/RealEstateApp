using RealEstateApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Domain.Entities
{
    public class Favorite : AuditableBaseEntity
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }

        #region Navigation Properties
        public Property Property { get; set; }
        #endregion
    }
}
