﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.DTO.Account
{
    public class RegisterResponse
    {
        public string UserId { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
