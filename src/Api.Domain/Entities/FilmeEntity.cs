using src.Api.Domain.Enuns;

namespace src.Api.Domain.Entities
{
    public class FilmeEntity : BaseEntity
    {
        public string Titulo { get; set; }
        public Categoria Categoria { get; set; }
        public int QtdLocacao { get; set; }  
        public UsuarioEntity Locatario { get; set; } 
        public FuncionarioEntity Cadastrador { get; set; }     
    }
}