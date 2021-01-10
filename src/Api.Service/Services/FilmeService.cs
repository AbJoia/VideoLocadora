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
        private readonly IRepository<FuncionarioEntity> _funcionarioRepository;
        private  readonly IRepository<UsuarioEntity> _usuarioRepository;
        private readonly IMapper _mapper;

        public FilmeService(IRepository<FilmeEntity> repository,
                            IRepository<FuncionarioEntity> funcionarioRepository,
                            IRepository<UsuarioEntity> usuarioRepository,
                            IMapper mapper)
        {
            _repository = repository;
            _funcionarioRepository = funcionarioRepository;
            _usuarioRepository = usuarioRepository;
            _mapper =mapper;
        }        

        public async Task<bool> DeleteAsync(Guid id)
        {
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
            var result = await _repository.SelectAsync();
            if(result == null) return null;
            return _mapper.Map<FilmeDtoGetResult>(result);
        }

        public async Task<FilmeDtoCreateResult> PostAsync(FilmeDto filme, Guid cadastrador)
        {
            var funcionario = await _funcionarioRepository.SelectAsync(cadastrador);
            if(funcionario == null) return null;
            var funcionarioModel = _mapper.Map<FuncionarioModel>(funcionario);
            var filmeModel = _mapper.Map<FilmeModel>(filme);
            filmeModel.Funcionario = funcionarioModel;
            filmeModel.QtdLocacao = 0;
            var filmeEntity = _mapper.Map<FilmeEntity>(filmeModel);
            var result = await _repository.InsertAsync(filmeEntity);
            if(result == null) return null;
            return _mapper.Map<FilmeDtoCreateResult>(result);
        }

        public async Task<FilmeDtoUpdateResult> PutAsync(FilmeDto filme, Guid cadastrador)
        {
            var funcionario = await _funcionarioRepository.SelectAsync(cadastrador);
            if(funcionario == null) return null;
            var funcionarioModel = _mapper.Map<FuncionarioModel>(funcionario);
            var filmeModel = _mapper.Map<FilmeModel>(filme);
            filmeModel.Funcionario = funcionarioModel;
            var filmeEntity = _mapper.Map<FilmeEntity>(filmeModel);
            var result = await _repository.UpdateAsync(filmeEntity);
            if(result == null) return null;
            return _mapper.Map<FilmeDtoUpdateResult>(result);
        }

        public async Task<FilmeDtoLocacaoResult> AluguelFilme(Guid IdLocatario, Guid IdFilme)
        {
            var locatarioEntity = await _usuarioRepository.SelectAsync(IdLocatario);
            if(locatarioEntity == null) return null;
            var locatarioModel = _mapper.Map<UsuarioModel>(locatarioEntity);
            var filmeEntity = await _repository.SelectAsync(IdFilme);
            if(filmeEntity == null) return null;
            var filmeModel = _mapper.Map<FilmeModel>(filmeEntity);
            filmeModel.Usuario = locatarioModel;
            filmeModel.QtdLocacao ++;
            filmeEntity = _mapper.Map<FilmeEntity>(filmeModel);
            var result = await _repository.UpdateAsync(filmeEntity); //Possivel Ajuste
            if(result == null) return null;
            return _mapper.Map<FilmeDtoLocacaoResult>(result); 
        }
    }
}