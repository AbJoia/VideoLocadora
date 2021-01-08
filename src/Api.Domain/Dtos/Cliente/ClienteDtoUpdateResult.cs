using System;
using src.Api.Domain.Enuns;

namespace src.Api.Domain.Dtos.Cliente
{
    public class ClienteDtoUpdateResult
    {
       public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; } 
    }
}