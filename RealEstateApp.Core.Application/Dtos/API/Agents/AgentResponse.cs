using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Dtos.API.Agents
{
    public class AgentResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int PropertiesCount { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
