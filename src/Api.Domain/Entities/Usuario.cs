using src.Api.Domain.Enuns;

namespace src.Api.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoUsuario tipoUsuario { get; set; }
    }
}