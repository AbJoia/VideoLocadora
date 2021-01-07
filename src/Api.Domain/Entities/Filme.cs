using src.Api.Domain.Enuns;

namespace src.Api.Domain.Entities
{
    public class Filme : BaseEntity
    {
        public string Titulo { get; set; }
        public Categoria Categoria { get; set; }
        public int QtdLocacao { get; set; }        
    }
}