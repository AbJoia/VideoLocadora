using System;
using System.ComponentModel.DataAnnotations;

namespace src.Api.Domain.Dtos.Aluguel
{
    public class AluguelDtoUpdate
    {
        [Required(ErrorMessage = "Campo Id é obrigatório.")] 
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo Usuário é obrigatório.")]
        public Guid UsuarioId { get; set; }

        public DateTime DataDevolucao { get; set; }  
    }
}