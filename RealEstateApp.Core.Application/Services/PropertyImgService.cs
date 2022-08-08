using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.PropertyImg;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class PropertyImgService : GenericService<PropertyImg, PropertyImgViewModel, SavePropertyImgViewModel>, IPropertyImgService
    {
        private readonly IPropertyImgRepository _propImgRepository;
        private readonly IMapper _mapper;

        public PropertyImgService(IPropertyImgRepository repo, IMapper mapper):base(repo,mapper)
        {
            _propImgRepository = repo;
            _mapper = mapper;
        }
    }
}
