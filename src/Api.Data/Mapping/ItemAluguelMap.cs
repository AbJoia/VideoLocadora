using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using src.Api.Domain.Entities;

namespace src.Api.Data.Mapping
{
    public class ItemAluguelMap : IEntityTypeConfiguration<ItemAluguelEntity>
    {
        public void Configure(EntityTypeBuilder<ItemAluguelEntity> builder)
        {
            builder.ToTable("item_aluguel");

            builder.HasKey(i => i.Id);

            builder.HasIndex(i => i.AluguelId);

            builder.Property(i => i.AluguelId)
                   .IsRequired();
            
            builder.Property(i => i.FilmeId)
                   .IsRequired();

            builder.HasOne(i => i.Aluguel)
                   .WithMany(a => a.ItensAluguel);
            
            builder.HasOne(i => i.Filme);                 
                    
        }
    }
}