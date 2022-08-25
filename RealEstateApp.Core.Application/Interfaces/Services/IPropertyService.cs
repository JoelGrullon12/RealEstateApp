﻿using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.Property;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<PropertyViewModel, SavePropertyViewModel>
    {
        Task<List<PropertyViewModel>> GetAllViewModelFromUser();
        //Task<List<PropertyViewModel>> GetProperties();

        Task<List<PropertyViewModel>> GetAllViewModelWithFilters(FilterPropertyViewModel filters);

        //Task<List<PropertyViewModel>> GetAllViewModel();
    }
}