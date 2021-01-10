using System;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Dtos.Filme
{
    public class FilmeDtoLocacaoResult
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public Categoria Categoria { get; set; }
        public UsuarioDtoLocacaoResult Locatario { get; set; }
    }
}