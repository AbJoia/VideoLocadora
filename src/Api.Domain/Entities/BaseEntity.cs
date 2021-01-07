using System;

namespace src.Api.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}