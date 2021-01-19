using Microsoft.EntityFrameworkCore;
using src.Api.Data.Mapping;
using src.Api.Domain.Entities;

namespace src.Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<FilmeEntity> Filme { get; set; }
        public DbSet<FuncionarioEntity> Funcionario { get; set; }
        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<AluguelEntity> Aluguel { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {            
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UsuarioEntity>(new UsuarioMap().Configure);
            modelBuilder.Entity<AluguelEntity>(new AluguelMap().Configure);
            modelBuilder.Entity<ItemAluguelEntity>(new ItemAluguelMap().Configure);
            modelBuilder.Entity<FuncionarioEntity>(new FuncionarioMap().Configure);
            modelBuilder.Entity<FilmeEntity>(new FilmeMap().Configure);                        
        }
    }
}