﻿using RealEstateApp.Core.Domain.Common;
using System.Collections.Generic;

namespace RealEstateApp.Core.Domain.Entities
{
    public class SellType : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        #region Navigation Properties
        public ICollection<Property> Properties { get; set; }
        #endregion
    }
}