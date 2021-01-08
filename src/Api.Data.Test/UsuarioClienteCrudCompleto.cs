using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using src.Api.Data.Context;
using src.Api.Data.Repository;
using src.Api.Domain.Entities;
using src.Api.Domain.Enuns;
using Xunit;

namespace src.Api.Data.Test
{
    public class UsuarioClienteCrudCompleto : BaseTeste, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioClienteCrudCompleto(DbTest dbTest)
        {
            _serviceProvider = dbTest.serviceProvider;            
        }

        [Fact]
        public async Task E_Possivel_Realizar_Crud_Usuario_Cliente()
        {
            using(var context = _serviceProvider.GetService<MyContext>())
            {
                BaseRepository<UsuarioEntity> _repository = new BaseRepository<UsuarioEntity>(context);
                var usuario = new UsuarioEntity()
                {
                    Nome = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    TipoUsuario = TipoUsuario.CLIENTE
                };

                //Post
                var insertResult = await _repository.InsertAsync(usuario);
                Assert.NotNull(insertResult);
                Assert.True(insertResult.Id != Guid.Empty);
                Assert.Equal(insertResult.Nome, usuario.Nome);
                Assert.Equal(insertResult.Email, usuario.Email);
                Assert.Equal(insertResult.TipoUsuario, usuario.TipoUsuario);
                Assert.IsType<TipoUsuario>(usuario.TipoUsuario);                
                Assert.Null(usuario.FilmesAlugados);

                //Update                
                usuario.Nome = Faker.Name.FullName();
                usuario.Email =Faker.Internet.Email();                  
                
                var updateResult = await _repository.UpdateAsync(usuario);
                Assert.NotNull(updateResult);
                Assert.Equal(updateResult.Id, insertResult.Id);
                Assert.Equal(updateResult.Nome, usuario.Nome);
                Assert.Equal(updateResult.Email, usuario.Email);
                Assert.True(updateResult.UpdateAt.CompareTo(insertResult.CreateAt) > 0);
                Assert.True(updateResult.CreateAt.CompareTo(insertResult.CreateAt) == 0);

                //GetId
                var getResult = await _repository.SelectAsync(usuario.Id);
                Assert.NotNull(getResult);
                Assert.Equal(getResult.Id, usuario.Id);
                Assert.Equal(getResult.Nome, usuario.Nome);
                Assert.Equal(getResult.Email, usuario.Email);

                //GetAll
                var getAllResult = await _repository.SelectAsync();
                Assert.NotNull(getAllResult);
                Assert.True(getAllResult.Count() > 0);

                //Delete
                var deleteResult = await _repository.DeleteAsync(usuario.Id);
                Assert.True(deleteResult);               

            }
        }        
    }
}