using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}
