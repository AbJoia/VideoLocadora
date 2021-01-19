using Microsoft.EntityFrameworkCore;
using src.Api.Domain.Entities;

namespace src.Api.Data.Mapping
{
    public class FilmeMap : IEntityTypeConfiguration<FilmeEntity>
    {
       public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FilmeEntity> builder)
       {
       builder.ToTable("filme");
       builder.HasKey(u => u.Id);

       builder.Property(u => u.Titulo)
              .IsRequired()
              .HasMaxLength(100); 
       
       builder.Property(u => u.Categoria)
              .IsRequired();             
       
       builder.HasOne(f => f.Funcionario)
              .WithMany(func => func.FilmesCadastrados);                    
       }
    }
}