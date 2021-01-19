using System;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Entities;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Dtos.Filme
{
    public class FilmeDtoGetResult
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string Titulo { get; set; }
        public Categoria Categoria { get; set; }
        public int QtdLocacao { get; set; }
        public FuncionarioDtoGetResult Funcionario { get; set; }
    }
}