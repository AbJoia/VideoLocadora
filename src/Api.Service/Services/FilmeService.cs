using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces;
using src.Api.Domain.Interfaces.Services;
using src.Api.Domain.Model;

namespace src.Api.Service.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IRepository<FilmeEntity> _repository;
        
        private readonly IMapper _mapper;

        public FilmeService(IRepository<FilmeEntity> repository, IMapper mapper)
        {
            _repository = repository;            
            _mapper =mapper;
        }        

        public async Task<bool> DeleteAsync(Guid id)
        {
            if(id == default(Guid)) return false;
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<FilmeDtoGetResult>> GetAllAsync()
        {
            var result = await _repository.SelectAsync();
            if(result == null) return null;
            return _mapper.Map<IEnumerable<FilmeDtoGetResult>>(result);
        }

        public async Task<FilmeDtoGetResult> GetAsync(Guid id)
        {
            if(id == default(Guid)) return null;
            var result = await _repository.SelectAsync();
            if(result == null) return null;
            return _mapper.Map<FilmeDtoGetResult>(result);
        }

        public async Task<FilmeDtoCreateResult> PostAsync(FilmeDto filme)
        {
            if(filme == null) return null;            
            var filmeModel = _mapper.Map<FilmeModel>(filme);            
            filmeModel.QtdLocacao = 0;
            var filmeEntity = _mapper.Map<FilmeEntity>(filmeModel);
            var result = await _repository.InsertAsync(filmeEntity);
            if(result == null) return null;
            return _mapper.Map<FilmeDtoCreateResult>(result);
        }

        public async Task<FilmeDtoUpdateResult> PutAsync(FilmeDtoUpdate filme)
        {
            if(filme == null) return null;           
            var filmeModel = _mapper.Map<FilmeModel>(filme);                        
            var filmeEntity = _mapper.Map<FilmeEntity>(filmeModel);
            var result = await _repository.UpdateAsync(filmeEntity);
            if(result == null) return null;
            return _mapper.Map<FilmeDtoUpdateResult>(result);
        }        
    }
}