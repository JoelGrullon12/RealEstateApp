using AutoMapper;
using RealEstateApp.Core.Application.DTO.Account;
using RealEstateApp.Core.Application.DTO.API.Properties;
using RealEstateApp.Core.Application.DTO.API.PropertyTypes;
using RealEstateApp.Core.Application.DTO.API.SellTypes;
using RealEstateApp.Core.Application.DTO.API.Upgrades;
using RealEstateApp.Core.Application.Features.Properties.Queries.ListProperties;
using RealEstateApp.Core.Application.Features.PropertyTypes.Commands.CreatePropertyType;
using RealEstateApp.Core.Application.Features.PropertyTypes.Commands.UpdatePropertyType;
using RealEstateApp.Core.Application.Features.SellTypes.Commands.CreateSellType;
using RealEstateApp.Core.Application.Features.SellTypes.Commands.UpdateSellType;
using RealEstateApp.Core.Application.Features.Upgrades.Commands.CreateUpgrade;
using RealEstateApp.Core.Application.Features.Upgrades.Commands.UpdateUpgrade;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.PropertyImg;
using RealEstateApp.Core.Application.ViewModels.PropertyType;
using RealEstateApp.Core.Application.ViewModels.PropertyUpgrade;
using RealEstateApp.Core.Application.ViewModels.SellType;
using RealEstateApp.Core.Application.ViewModels.Upgrade;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Mappings
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            // ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // Identity

            ///<Entity>
            /// Login
            /// </Entity>
            CreateMap<LoginViewModel, LoginRequest>()
                .ReverseMap()
                ;

            ///<Entity>
            /// User - Register
            /// </Entity>
            CreateMap<SaveUserViewModel, RegisterRequest>()
                .ForMember(dest => dest.CurrentPassword, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                ;


            // ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // ViewModels and Entities

            ///<Entity>
            /// Favorite
            /// </Entity>
            CreateMap<Favorite, FavoriteViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                ;

            CreateMap<Favorite, SaveFavoriteViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Property, opt => opt.Ignore())
                ;

            ///<Entity>
            /// Property
            /// </Entity>
            CreateMap<Property, PropertyViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.PropertyUpgrades, opt => opt.Ignore())
                ;

            CreateMap<Property, SavePropertyViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.SellType, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Upgrades, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyUpgrades, opt => opt.Ignore())
                .ForMember(dest => dest.Favorites, opt => opt.Ignore())
                ;

            ///<Entity>
            /// PropertyImg
            /// </Entity>
            CreateMap<PropertyImg, PropertyImgViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                ;

            CreateMap<PropertyImg, SavePropertyImgViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Property, opt => opt.Ignore())
                ;

            ///<Entity>
            /// PropertyType
            /// </Entity>
            CreateMap<PropertyType, PropertyTypeViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                ;

            CreateMap<PropertyType, SavePropertyTypeViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                ;

            ///<Entity>
            /// PropertyUpgrade
            /// </Entity>
            CreateMap<PropertyUpgrade, PropertyUpgradeViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                ;

            CreateMap<PropertyUpgrade, SavePropertyUpgradeViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Property, opt => opt.Ignore())
                .ForMember(dest => dest.Upgrade, opt => opt.Ignore())
                ;

            ///<Entity>
            /// SellType
            /// </Entity>
            CreateMap<SellType, SellTypeViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                ;

            CreateMap<SellType, SaveSellTypeViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                ;

            ///<Entity>
            /// Upgrade
            /// </Entity>
            CreateMap<Upgrade, UpgradeViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyUpgrades, opt => opt.Ignore())
                ;

            CreateMap<Upgrade, SaveUpgradeViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyUpgrades, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                ;


            // ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // CQRS DTOs - Entities

            ///<Entity>
            /// Property
            /// </Entity>
            CreateMap<Property, PropertyResponse>()
                .ForMember(dest => dest.Agent, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.TypeId, opt => opt.Ignore())
                .ForMember(dest => dest.AgentId, opt => opt.Ignore())
                .ForMember(dest => dest.SellTypeId, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyUpgrades, opt => opt.Ignore())
                .ForMember(dest => dest.Favorites, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                ;

            ///<Entity>
            /// PropertyType
            /// </Entity>
            CreateMap<PropertyType, PropertyTypeResponse>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                ;

            CreateMap<PropertyType, CreatePropertyTypeCommand>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                ;

            CreateMap<PropertyType, UpdatePropertyTypeCommand>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                ;

            ///<Entity>
            /// SellType
            /// </Entity>
            CreateMap<SellType, SellTypeResponse>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                ;

            CreateMap<SellType, CreateSellTypeCommand>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                ;

            CreateMap<SellType, UpdateSellTypeCommand>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                ;

            ///<Entity>
            /// Upgrade
            /// </Entity>
            CreateMap<Upgrade, UpgradeResponse>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyUpgrades, opt => opt.Ignore())
                ;

            CreateMap<Upgrade, CreateUpgradeCommand>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyUpgrades, opt => opt.Ignore())
                ;

            CreateMap<Upgrade, UpdateUpgradeCommand>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyUpgrades, opt => opt.Ignore())
                ;
        }
    }
}