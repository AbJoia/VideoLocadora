using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Entities;
using src.Api.Domain.Interfaces;
using src.Api.Domain.Interfaces.Services;
using src.Api.Domain.Model;

namespace src.Api.Service.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IRepository<FuncionarioEntity> _repository;
        private readonly IMapper _mapper;

        public FuncionarioService(IRepository<FuncionarioEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }        

        public async Task<IEnumerable<FuncionarioDtoGetResult>> GetAllAsync()
        {
            var result = await _repository.SelectAsync();
            if(result == null) return null;
            return _mapper.Map<IEnumerable<FuncionarioDtoGetResult>>(result);
        }

        public async Task<FuncionarioDtoGetResult> GetAsync(Guid id)
        {
            if(id == Guid.Empty || id == null) return null;
            var result = await _repository.SelectAsync(id);
            if(result == null) return null;
            return _mapper.Map<FuncionarioDtoGetResult>(result);
        }

        public async Task<FuncionarioDtoCreateResult> PostAsync(FuncionarioDto funcionario)
        {
            if(funcionario == null) return null;
            var model = _mapper.Map<FuncionarioModel>(funcionario);
            model.Matricula = GerarMatricula(); 
            model.TipoUsuario = Domain.Enuns.TipoUsuario.FUNCIONARIO;           
            var entity = _mapper.Map<FuncionarioEntity>(model);
            var result = await _repository.InsertAsync(entity);
            if(result == null) return null;
            return _mapper.Map<FuncionarioDtoCreateResult>(result);
        }

        public async Task<FuncionarioDtoUpdateResult> PutAsync(FuncionarioDtoUpdate funcionario)
        {
            if(funcionario == null) return null;
            var matricula = _repository.SelectAsync(funcionario.Id).Result.Matricula;
            var model = _mapper.Map<FuncionarioModel>(funcionario);
            model.Matricula = matricula;
            var entity = _mapper.Map<FuncionarioEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            if(result == null) return null;
            return _mapper.Map<FuncionarioDtoUpdateResult>(result);
        }

        private int GerarMatricula()
        {
            return new Random().Next(1000, 9999);
        }        
    }
}