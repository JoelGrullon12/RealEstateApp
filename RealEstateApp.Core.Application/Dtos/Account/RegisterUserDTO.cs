using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Dtos.Account
{
    public class RegisterUserDTO
    {
        public object User { get; set; }
        public RegisterResponse Response { get; set; }
    }
}
