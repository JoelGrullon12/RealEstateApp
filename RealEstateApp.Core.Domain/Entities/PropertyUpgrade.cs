using RealEstateApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Domain.Entities
{
    public class PropertyUpgrade : AuditableBaseEntity
    {
        public int PropertyId { get; set; }
        public int UpgradeId { get; set; }

        #region Navigation Properties
        public Property Property { get; set; }
        public Upgrade Upgrade { get; set; }
        #endregion
    }
}
