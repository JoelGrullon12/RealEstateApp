using System;

namespace RealEstateApp.Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}