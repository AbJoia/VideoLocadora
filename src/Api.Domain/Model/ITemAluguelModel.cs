using System;

namespace src.Api.Domain.Model
{
    public class ITemAluguelModel : BaseModel
    {
        private Guid aluguelId;
        public Guid AluguelId
        {
            get { return aluguelId; }
            set { aluguelId = value; }
        }
        
        private Guid filmeId;
        public Guid FilmeId
        {
            get { return filmeId; }
            set { filmeId = value; }
        }        
    }
}