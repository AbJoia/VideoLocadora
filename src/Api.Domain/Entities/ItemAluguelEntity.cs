using System;

namespace src.Api.Domain.Entities
{
    public class ItemAluguelEntity : BaseEntity
    {
        public Guid AluguelId { get; set; }
        public AluguelEntity Aluguel { get; set; }
        public Guid FilmeId { get; set; }
        public FilmeEntity Filme { get; set; }
    }
}