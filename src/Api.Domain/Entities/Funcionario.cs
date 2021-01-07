namespace src.Api.Domain.Entities
{
    public class Funcionario : Usuario
    {
        public string senha { get; set; }
        public long matricula { get; set; }
    }
}