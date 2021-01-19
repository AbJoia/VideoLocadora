using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using src.Api.Data.Context;
using src.Api.Data.Implementation;
using src.Api.Data.Repository;
using src.Api.Domain.Entities;
using Xunit;

namespace src.Api.Data.Test
{
    public class AluguelCrudCompleto : BaseTeste, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public AluguelCrudCompleto(DbTest dbTest)
        {
            _serviceProvider = dbTest.serviceProvider;
        }

        [Fact]
        public async Task E_Possivel_Realizar_Crud_Aluguel()
        {
            using(var context = _serviceProvider.GetService<MyContext>())
            {
                BaseRepository<UsuarioEntity> _usuarioRepository = new BaseRepository<UsuarioEntity>(context);
                AluguelImplementation _repository = new AluguelImplementation(context);

                var usuario = await _usuarioRepository.InsertAsync(
                    new UsuarioEntity
                    {                        
                        Nome = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                    }
                );

                var aluguel = new AluguelEntity
                {
                    UsuarioId = usuario.Id,
                    DataDevolução = DateTime.UtcNow.AddHours(72.0),
                };

                //Post
                var postResult = await _repository.InsertAsync(aluguel);
                Assert.NotNull(postResult);
                Assert.True(postResult.UsuarioId != default(Guid));
                Assert.Equal(aluguel.UsuarioId, usuario.Id);
                Assert.Equal(postResult.DataDevolução, aluguel.DataDevolução);
                Assert.True(aluguel.Id != default(Guid));
                Assert.True(postResult.CreateAt != default(DateTime));

                aluguel.Id = Guid.Empty;
                postResult = await _repository.RealizarAluguel(aluguel);
                Assert.NotNull(postResult);
                Assert.True(postResult.UsuarioId != default(Guid));
                Assert.Equal(aluguel.UsuarioId, usuario.Id);
                Assert.Equal(postResult.DataDevolução, aluguel.DataDevolução);
                Assert.True(aluguel.Id != default(Guid));
                Assert.True(postResult.CreateAt != default(DateTime));

                //Put
                aluguel.Id = postResult.Id;
                aluguel.DataDevolução = DateTime.UtcNow.AddHours(120.0);                
                var putResult = await _repository.UpdateAsync(aluguel);
                Assert.NotNull(putResult);
                Assert.Equal(putResult.Id, aluguel.Id);
                Assert.Equal(putResult.DataDevolução, aluguel.DataDevolução);
                Assert.True(putResult.UpdateAt.CompareTo(postResult.CreateAt) > 0);

                //Get
                var getResult = await _repository.SelectAsync(aluguel.Id);
                Assert.NotNull(getResult);
                Assert.Equal(getResult.Id, aluguel.Id);
                Assert.Equal(getResult.DataDevolução, aluguel.DataDevolução);

                //GetAll
                var getAll = await _repository.SelectAsync();
                Assert.NotNull(getAll);
                Assert.True(getAll.Count() > 0);
                Assert.True(getAll.Where(a => a.Id == aluguel.Id).Count() == 1);

                //GetAllByUsuarioId
                var getAllByUsuarioId = await _repository.GetAllByUsuarioId(usuario.Id);
                Assert.NotNull(getAllByUsuarioId);
                Assert.True(getAllByUsuarioId.Count() > 0); 

                //Delete
                var deleteResult = await _repository.DeleteAsync(aluguel.Id);                
                Assert.True(deleteResult);              

            }            
        }         
    }
}