using System;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Entities;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Dtos.Filme
{
    public class FilmeDtoCreateResult
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public string Titulo { get; set; }
        public Categoria Categoria { get; set; }
        public Guid FuncionarioId { get; set; }
    }
}