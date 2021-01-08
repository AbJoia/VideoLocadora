using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using src.Api.Domain.Entities;

namespace src.Api.Data.Mapping
{
    public class FuncionarioMap : IEntityTypeConfiguration<FuncionarioEntity>
    {
        public void Configure(EntityTypeBuilder<FuncionarioEntity> builder)
        {                       

            builder.HasIndex(u => u.Matricula)
                   .IsUnique();
            
            builder.HasIndex(u => u.Email)
                   .IsUnique();
            
            builder.Property(u => u.Nome)
                   .IsRequired()
                   .HasMaxLength(100); 
            
            builder.Property(u => u.Senha)
                   .IsRequired()
                   .HasMaxLength(100);
            
            builder.Property(u => u.TipoUsuario)
                   .IsRequired();                  
        }
    }
}