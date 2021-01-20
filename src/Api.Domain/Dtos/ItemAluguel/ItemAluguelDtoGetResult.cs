using System;
using src.Api.Domain.Dtos.Filme;

namespace src.Api.Domain.Dtos.ItemAluguel
{
    public class ItemAluguelDtoGetResult
    {
        public Guid Id { get; set; }
        public FilmeDtoLocacaoResult Filme { get; set; }
    }
}