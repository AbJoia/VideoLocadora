using System;
using src.Api.Domain.Dtos.Usuario;

namespace src.Api.Domain.Dtos.Aluguel
{
    public class AluguelDtoUpdateResult
    {
        public Guid AluguelId { get; set; }
        public DateTime UpdateAt { get; set; }
        public UsuarioDtoGetResult Usuario { get; set; }       
        public DateTime DataDevolucao { get; set; }
    }
}