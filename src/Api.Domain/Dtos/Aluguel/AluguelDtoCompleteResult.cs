using System;
using System.Collections.Generic;
using src.Api.Domain.Dtos.ItemAluguel;
using src.Api.Domain.Dtos.Usuario;

namespace src.Api.Domain.Dtos.Aluguel
{
    public class AluguelDtoCompleteResult
    {
        public Guid AluguelId { get; set; }
        public UsuarioDtoGetResult Usuario { get; set; }
        public IEnumerable<ItemAluguelDtoGetResult> Itens { get; set; }
        public DateTime DataDevolucao { get; set; }
    }
}