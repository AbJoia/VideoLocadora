using System;
using System.Collections.Generic;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Model
{
    public class FuncionarioModel
    {
        private Guid id;
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }
        
        private DateTime createAt;
        public DateTime CreateAt
        {
            get { return createAt; }
            set { createAt = value == null? DateTime.UtcNow : value; }
        }
        
        private DateTime updateAt;
        public DateTime UpdateAt
        {
            get { return updateAt; }
            set { updateAt = value; }
        }
        
        private string nome;
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        
        private TipoUsuario tipoUsuario;
        public TipoUsuario TipoUsuario
        {
            get { return tipoUsuario; }
            set { tipoUsuario = value; }
        }

        private long matricula;
        public long Matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }

        private string senha;
        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        private IEnumerable<FilmeDtoGetResult> filmesCadastrados;
        public IEnumerable<FilmeDtoGetResult> FilmesCadastrados
        {
            get { return filmesCadastrados; }
            set { filmesCadastrados = value; }
        }      
        
        private IEnumerable<FilmeModel> filmesAlugados;
        public IEnumerable<FilmeModel> FilmesAlugados
        {
            get { return filmesAlugados; }
            set { filmesAlugados = value; }
        } 
    }
}