﻿using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class FavoriteService: GenericService<Favorite, FavoriteViewModel, SaveFavoriteViewModel>, IFavoriteService
    {
        private readonly IFavoriteRepository _favRepository;
        private readonly IMapper _mapper;

        public FavoriteService(IFavoriteRepository repo, IMapper mapper):base(repo,mapper)
        {
            _favRepository = repo;
            _mapper = mapper;
        }
    }
}