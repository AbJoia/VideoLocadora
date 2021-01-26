using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces;
using src.Api.Domain.Interfaces.Services;
using src.Api.Domain.Model;

namespace src.Api.Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepository<UsuarioEntity> _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IRepository<UsuarioEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UsuarioDtoGetResult>> GetAllAsync()
        {
            var result = await _repository.SelectAsync();
            if(result == null) return null;
            return _mapper.Map<IEnumerable<UsuarioDtoGetResult>>(result); 
        }

        public async Task<UsuarioDtoGetResult> GetAsync(Guid id)
        {
            var result = await _repository.SelectAsync(id);
            if(result == null) return null;
            return _mapper.Map<UsuarioDtoGetResult>(result);
        }

        public async Task<UsuarioDtoCreateResult> PostAsync(UsuarioDto usuario)
        {
            var model = _mapper.Map<UsuarioModel>(usuario);
            model.TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE;
            var entity = _mapper.Map<UsuarioEntity>(model);
            var result = await _repository.InsertAsync(entity);
            if(result == null) return null;
            return _mapper.Map<UsuarioDtoCreateResult>(result);
        }

        public async Task<UsuarioDtoUpdateResult> PutAsync(UsuarioDtoUpdate usuario)
        {
            var model = _mapper.Map<UsuarioModel>(usuario);
            model.TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE;
            var entity = _mapper.Map<UsuarioEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            if(result == null) return null;
            return _mapper.Map<UsuarioDtoUpdateResult>(result);
        }
    }
}