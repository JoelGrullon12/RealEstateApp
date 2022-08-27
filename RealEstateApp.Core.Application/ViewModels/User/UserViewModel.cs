using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DNI { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string ImgUrl { get; set; }
        public bool IsActive { get; set; }
        public int PropertiesCount { get; set; }
    }
}