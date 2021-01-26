using System;

namespace src.Api.Domain.Model
{
    public class AluguelModel : BaseModel
    {
        private Guid usuarioId;
        public Guid UsuarioId
        {
            get { return usuarioId; }
            set { usuarioId = value; }
        }

        private DateTime dataDevolucao;
        public DateTime DataDevolucao
        {
            get { return dataDevolucao; }
            set { dataDevolucao = value == default(DateTime)? DateTime.UtcNow.AddHours(72.0) : value;}
        }       
    }
}