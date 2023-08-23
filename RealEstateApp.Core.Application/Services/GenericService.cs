using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class GenericService<Entity, ViewModel, SaveViewModel> : IGenericService<ViewModel, SaveViewModel>
        where Entity : class
        where ViewModel : class
        where SaveViewModel : class
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            entity = await _repository.AddAsync(entity);
            SaveViewModel saveViewModel = _mapper.Map<SaveViewModel>(entity);
            return saveViewModel;
        }

        public virtual async Task Update(SaveViewModel saveViewModel, int id)
        {
            Entity entity = _mapper.Map<Entity>(saveViewModel);
            await _repository.UpdateAsync(entity, id);
        }

        public virtual async Task<List<ViewModel>> GetAllViewModel()
        {
            List<Entity> entities = await _repository.GetAllAsync();
            return _mapper.Map<List<ViewModel>>(entities);
        }

        public virtual async Task<SaveViewModel> GetByIdSaveViewModel(int id)
        {
            Entity entity = await _repository.GetByIdAsync(id);
            SaveViewModel saveViewModel = _mapper.Map<SaveViewModel>(entity);
            return saveViewModel;
        }

        public virtual async Task Delete(int id)
        {
            Entity t = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(t);
        }
    }
}