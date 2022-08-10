using AutoMapper;
using RealEstateApp.Core.Application.DTO.Account;
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


        }
    }
}
