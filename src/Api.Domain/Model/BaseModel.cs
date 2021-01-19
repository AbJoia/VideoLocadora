using System;

namespace src.Api.Domain.Model
{
    public class BaseModel
    {
        private Guid id;
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        private DateTime createAt;
        public DateTime CreateAt
        {
            get { return createAt; }
            set { createAt = value == null? DateTime.UtcNow : value; }
        }

        private DateTime updateAt;
        public DateTime UpdateAt
        {
            get { return updateAt; }
            set { updateAt = value; }
        }       
    }
}