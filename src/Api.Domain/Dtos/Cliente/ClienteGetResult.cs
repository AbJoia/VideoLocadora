using System;
using System.Collections;
using System.Collections.Generic;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Dtos.Cliente
{
    public class ClienteGetResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; } 
        public IEnumerable<FilmeDtoGetResult> FilmesAlugados { get; set; }
    }
}