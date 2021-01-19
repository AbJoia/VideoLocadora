using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using src.Api.Domain.Entities;

namespace src.Api.Data.Mapping
{
    public class AluguelMap : IEntityTypeConfiguration<AluguelEntity>
    {
        public void Configure(EntityTypeBuilder<AluguelEntity> builder)
        {
            builder.ToTable("aluguel");

            builder.HasKey(a => a.Id);

            builder.HasIndex(a => a.UsuarioId);

            builder.HasOne(a => a.Usuario)
                   .WithMany(u => u.Alugueis);          
        }
    }
}