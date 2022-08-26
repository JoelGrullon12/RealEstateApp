﻿using RealEstateApp.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
    }
}