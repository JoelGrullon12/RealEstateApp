using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.PropertyType;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class PropertyTypeService : GenericService<PropertyType, PropertyTypeViewModel, SavePropertyTypeViewModel>, IPropertyTypeService
    {
        private readonly IPropertyTypeRepository _propTypeRepository;
        private readonly IMapper _mapper;

        public PropertyTypeService(IPropertyTypeRepository repo, IMapper mapper):base(repo,mapper)
        {
            _propTypeRepository = repo;
            _mapper = mapper;
        }

        public async Task Update(SavePropertyTypeViewViewModel vm)
        {
            PropertyType PropertyType = await _propTypeRepository.GetByIdAsync(vm.id);
            categoria.Id = vm.Id;
            categoria.Nombre = vm.Nombre;
            categoria.Descripcion = vm.Descripcion;

            await _categoriaRepository.UpdateAsync(categoria);
        }

        public async Task<SaveCategoriaViewModel> Add(SaveCategoriaViewModel vm)
        {
            Categoria categoria = new();
            categoria.Nombre = vm.Nombre;
            categoria.Descripcion = vm.Descripcion;

            categoria = await _categoriaRepository.AddAsync(categoria);

            SaveCategoriaViewModel categoriaVm = new();

            categoriaVm.Id = categoria.Id;
            categoriaVm.Nombre = categoria.Nombre;
            categoriaVm.Descripcion = categoria.Descripcion;

            return categoriaVm;
        }

        public async Task Delete(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            await _categoriaRepository.DeleteAsync(categoria);
        }

        public async Task<SaveCategoriaViewModel> GetByIdSaveViewModel(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);

            SaveCategoriaViewModel vm = new();
            vm.Id = categoria.Id;
            vm.Nombre = categoria.Nombre;
            vm.Descripcion = categoria.Descripcion;

            return vm;
        }

        public async Task<List<CategoriaViewModel>> GetAllViewModel()
        {
            var categoriaList = await _categoriaRepository.GetAllWithIncludeAsync(new List<string> { "Anuncios" });

            return categoriaList.Select(categoria => new CategoriaViewModel
            {
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                Id = categoria.Id,
                AnunciosQuantity = categoria.Anuncios.Where(anuncio => anuncio.UserId == userViewModel.Id).Count()
            }).ToList();
        }
    }
}
