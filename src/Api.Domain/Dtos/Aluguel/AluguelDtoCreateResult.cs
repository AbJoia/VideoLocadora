using System;
using System.Collections.Generic;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.ItemAluguel;
using src.Api.Domain.Dtos.Usuario;

namespace src.Api.Domain.Dtos.Aluguel
{
    public class AluguelDtoCreateResult    
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public UsuarioDtoGetResult Usuario { get; set; }       
        public DateTime DataDevolucao { get; set; }
    }
}