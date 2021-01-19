using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using src.Api.Domain.Dtos.ItemAluguel;

namespace src.Api.Domain.Dtos.Aluguel
{
    public class AluguelDto
    {
        [Required(ErrorMessage = "Campo Usuário é obrigatório.")]
        public Guid UsuarioId { get; set; }  
        public DateTime DataDevolucao { get; set; }      
    }
}