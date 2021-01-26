using System;
using src.Api.Domain.Dtos.Usuario;

namespace src.Api.Domain.Dtos.Aluguel
{
    public class AluguelDtoUpdateResult
    {
        public Guid Id { get; set; }
        public DateTime UpdateAt { get; set; }
        public Guid UsuarioId { get; set; }       
        public DateTime DataDevolucao { get; set; }
    }
}