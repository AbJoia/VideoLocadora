using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces.Repositories;
using src.Api.Domain.Interfaces.Services;
using src.Api.Domain.Model;

namespace src.Api.Service.Services
{
    public class AluguelService : IAluguelService
    {
        private IAluguelRepository _repository;
        private IMapper _mapper;

        public AluguelService(IAluguelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAluguelAsync(Guid id)
        {            
            if(id == default(Guid)) return false;
            return await _repository.DeleteAsync(id);            
        }

        public async Task<IEnumerable<AluguelDtoGetResult>> GetAllByUsuarioIdAsync(Guid usuarioId)
        {
            if(usuarioId == default(Guid)) return null;
            var result = await _repository.GetAllByUsuarioId(usuarioId);
            return _mapper.Map<IEnumerable<AluguelDtoGetResult>>(result);
        }

        public async Task<AluguelDtoCompleteResult> GetCompleteByIdAsync(Guid id)
        {
            if(id == default(Guid)) return null;
            var result = await _repository.GetCompleteById(id);
            return _mapper.Map<AluguelDtoCompleteResult>(result);
        }

        public async Task<AluguelDtoCreateResult> PostAluguelAsync(AluguelDto aluguel)
        {
            if(aluguel == null) return null;
            var model = _mapper.Map<AluguelModel>(aluguel);
            var entity = _mapper.Map<AluguelEntity>(model);
            var result = await _repository.RealizarAluguel(entity);
            return _mapper.Map<AluguelDtoCreateResult>(result);
        }

        public async Task<AluguelDtoUpdateResult> PutAluguelAsync(AluguelDtoUpdate aluguel)
        {
            if(aluguel == null) return null;
            var model = _mapper.Map<AluguelModel>(aluguel);
            var entity = _mapper.Map<AluguelEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<AluguelDtoUpdateResult>(result);
        }
    }
}