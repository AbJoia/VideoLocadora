using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using src.Api.Domain.Dtos.ItemAluguel;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces;
using src.Api.Domain.Interfaces.Repositories;
using src.Api.Domain.Interfaces.Services;
using src.Api.Domain.Model;

namespace src.Api.Service.Services
{
    public class ItemAluguelService : IItemAluguelService
    {
        private IItemAluguelRepository _repository;
        private IRepository<FilmeEntity> _filmeRepository;
        private IMapper _mapper;

        public ItemAluguelService(IItemAluguelRepository repository, 
                                  IRepository<FilmeEntity> filmeRepository,
                                  IMapper mapper)
        {
            _repository = repository;
            _filmeRepository = filmeRepository;
            _mapper =mapper;
        }

        public async Task<bool> DeleteItemAluguelAsync(Guid id)
        {
            if(id == default(Guid)) return false;
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ItemAluguelDtoGetResult>> GetAllItensByAluguelIdAsync(Guid aluguelId)
        {
            if(aluguelId == default(Guid)) return null;
            var result = await _repository.GetAllItensByAluguelId(aluguelId);            
            return _mapper.Map<IEnumerable<ItemAluguelDtoGetResult>>(result);
        }

        public async Task<ItemAluguelDtoCreateResult> PostItemAluguelAsync(ItemAluguelDto item)
        {
            if(item == null) return null;
            var model = _mapper.Map<ItemAluguelModel>(item);            
            var entity = _mapper.Map<ItemAluguelEntity>(model);
            var result = await _repository.InsertAsync(entity);
            if(result != null)
            {
                var filme = await _filmeRepository.SelectAsync(item.FilmeId);
                filme.QtdLocacao++;
                await _filmeRepository.UpdateAsync(filme);  
            }                      
            return _mapper.Map<ItemAluguelDtoCreateResult>(result);
        }

        public async Task<ItemAluguelDtoUpdateResult> PutItemAluguelAsync(ItemAluguelDtoUpdate item)
        {
            if(item == null) return null;
            var model = _mapper.Map<ItemAluguelModel>(item);
            var entity = _mapper.Map<ItemAluguelEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<ItemAluguelDtoUpdateResult>(result);
        }
    }
}