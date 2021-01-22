using System;
using System.Collections.Generic;
using System.Linq;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Entities;
using src.Api.Domain.Model;
using Xunit;

namespace src.Api.Service.Test.AutoMapper
{
    public class AluguelMapper : BaseTesteService
    {
        [Fact]
        public void E_Possivel_Mapear_Modelos_Aluguel()
        {
            var model = new AluguelModel
            {
                Id = Guid.NewGuid(),
                UsuarioId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow,
                DataDevolucao = DateTime.UtcNow.AddHours(72.0),
                UpdateAt = DateTime.UtcNow,
            };            

            List<AluguelEntity> listAluguel = new List<AluguelEntity>();
            for (int i = 0; i < new Random().Next(2, 10); i++)
            {
                var usuario = new UsuarioEntity
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    TipoUsuario = Domain.Enuns.TipoUsuario.CLIENTE,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow.AddHours(2.0)  
                };

                var filme = new FilmeEntity
                {
                    Id = Guid.NewGuid(),
                    Titulo = Faker.Country.Name(),
                    Categoria = Domain.Enuns.Categoria.AÇÃO,
                };

                var filme2 = new FilmeEntity
                {
                    Id = Guid.NewGuid(),
                    Titulo = Faker.Country.Name(),
                    Categoria = Domain.Enuns.Categoria.ROMANCE,
                };

                listAluguel.Add(
                    new AluguelEntity
                    {
                        Id = Guid.NewGuid(),
                        UsuarioId = usuario.Id,
                        CreateAt = DateTime.UtcNow,
                        DataDevolucao = DateTime.UtcNow.AddHours(72.0),
                        UpdateAt = DateTime.UtcNow.AddHours(2.0),
                        Usuario = usuario,
                        ItensAluguel = new List<ItemAluguelEntity>()
                        {
                            new ItemAluguelEntity
                            {
                                Id = Guid.NewGuid(),
                                FilmeId = Guid.NewGuid(),
                                CreateAt = DateTime.UtcNow, 
                                Filme = filme                               
                            },
                            new ItemAluguelEntity
                            {
                                Id = Guid.NewGuid(),
                                FilmeId = Guid.NewGuid(),
                                CreateAt = DateTime.UtcNow,
                                Filme = filme2
                            }
                        }

                    }
                );      
            }

            //Model => Entity
            var entity = Mapper.Map<AluguelEntity>(model);
            Assert.NotNull(entity);
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.UsuarioId, entity.UsuarioId);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);
            Assert.Equal(model.DataDevolucao, entity.DataDevolucao);

            //Entity => Dtos
            var dtoCreateResult = Mapper.Map<AluguelDtoCreateResult>(listAluguel[0]);
            Assert.NotNull(dtoCreateResult);
            Assert.NotNull(dtoCreateResult.Usuario);
            Assert.Equal(dtoCreateResult.Id, listAluguel[0].Id);
            Assert.Equal(dtoCreateResult.Usuario.Id, listAluguel[0].UsuarioId);
            Assert.Equal(dtoCreateResult.CreateAt, listAluguel[0].CreateAt);            
            Assert.Equal(dtoCreateResult.DataDevolucao, listAluguel[0].DataDevolucao);

            var dtoGetResult = Mapper.Map<AluguelDtoGetResult>(listAluguel[0]);
            Assert.NotNull(dtoGetResult);
            Assert.NotNull(dtoGetResult.Usuario);
            Assert.Equal(dtoGetResult.Id, listAluguel[0].Id);
            Assert.Equal(dtoGetResult.Usuario.Id, listAluguel[0].UsuarioId);
            Assert.Equal(dtoGetResult.Usuario.Nome, listAluguel[0].Usuario.Nome); 
            Assert.Equal(dtoGetResult.Usuario.Email, listAluguel[0].Usuario.Email);  
            Assert.Equal(dtoGetResult.Usuario.TipoUsuario, listAluguel[0].Usuario.TipoUsuario);
            Assert.Equal(dtoGetResult.Usuario.CreateAt, listAluguel[0].Usuario.CreateAt); 
            Assert.Equal(dtoGetResult.Usuario.UpdateAt, listAluguel[0].Usuario.UpdateAt);                                     
            Assert.Equal(dtoGetResult.DataDevolucao, listAluguel[0].DataDevolucao);

            var dtoCompleteResult = Mapper.Map<AluguelDtoCompleteResult>(listAluguel[0]);
            Assert.NotNull(dtoCompleteResult);
            Assert.NotNull(dtoCompleteResult.Usuario);
            Assert.NotNull(dtoCompleteResult.ItensAluguel);
            Assert.Equal(dtoCompleteResult.Id, listAluguel[0].Id);
            Assert.Equal(dtoCompleteResult.Usuario.Id, listAluguel[0].UsuarioId);
            Assert.Equal(dtoCompleteResult.Usuario.Id, listAluguel[0].Usuario.Id);
            Assert.Equal(dtoCompleteResult.Usuario.Nome, listAluguel[0].Usuario.Nome); 
            Assert.Equal(dtoCompleteResult.Usuario.Email, listAluguel[0].Usuario.Email);  
            Assert.Equal(dtoCompleteResult.Usuario.TipoUsuario, listAluguel[0].Usuario.TipoUsuario);
            Assert.Equal(dtoCompleteResult.Usuario.CreateAt, listAluguel[0].Usuario.CreateAt); 
            Assert.Equal(dtoCompleteResult.Usuario.UpdateAt, listAluguel[0].Usuario.UpdateAt);
            Assert.Equal(dtoCompleteResult.DataDevolucao, listAluguel[0].DataDevolucao);
            Assert.Equal(dtoCompleteResult.ItensAluguel.Count(), listAluguel[0].ItensAluguel.Count());

            for(int i = 0; i < dtoCompleteResult.ItensAluguel.Count(); i++)
            {
                Assert.Equal(dtoCompleteResult.ItensAluguel.ToList()[i].Id,
                             listAluguel[0].ItensAluguel.ToList()[i].Id);

                Assert.Equal(dtoCompleteResult.ItensAluguel.ToList()[i].Filme.Id,
                             listAluguel[0].ItensAluguel.ToList()[i].Filme.Id);

                Assert.Equal(dtoCompleteResult.ItensAluguel.ToList()[i].Filme.Titulo,
                             listAluguel[0].ItensAluguel.ToList()[i].Filme.Titulo);

                Assert.Equal(dtoCompleteResult.ItensAluguel.ToList()[i].Filme.Categoria,
                             listAluguel[0].ItensAluguel.ToList()[i].Filme.Categoria); 
            }

            var dtoUpdateResult = Mapper.Map<AluguelDtoUpdateResult>(listAluguel[0]);
            Assert.NotNull(dtoUpdateResult);
            Assert.NotNull(dtoUpdateResult.Usuario);
            Assert.Equal(dtoUpdateResult.Id, listAluguel[0].Id);
            Assert.Equal(dtoUpdateResult.Usuario.Id, listAluguel[0].UsuarioId);
            Assert.Equal(dtoUpdateResult.Usuario.Nome, listAluguel[0].Usuario.Nome); 
            Assert.Equal(dtoUpdateResult.Usuario.Email, listAluguel[0].Usuario.Email);  
            Assert.Equal(dtoUpdateResult.Usuario.TipoUsuario, listAluguel[0].Usuario.TipoUsuario);
            Assert.Equal(dtoUpdateResult.Usuario.CreateAt, listAluguel[0].Usuario.CreateAt); 
            Assert.Equal(dtoUpdateResult.Usuario.UpdateAt, listAluguel[0].Usuario.UpdateAt);                                     
            Assert.Equal(dtoUpdateResult.DataDevolucao, listAluguel[0].DataDevolucao);

            var listResult = Mapper.Map<IEnumerable<AluguelDtoGetResult>>(listAluguel);
            Assert.NotNull(listResult);
            for (int i = 0; i < listResult.Count(); i++)
            {
                Assert.Equal(listResult.ToList()[i].Id, listAluguel[i].Id);
                Assert.Equal(listResult.ToList()[i].Usuario.Id, listAluguel[i].UsuarioId);
                Assert.Equal(listResult.ToList()[i].Usuario.Nome, listAluguel[i].Usuario.Nome); 
                Assert.Equal(listResult.ToList()[i].Usuario.Email, listAluguel[i].Usuario.Email);  
                Assert.Equal(listResult.ToList()[i].Usuario.TipoUsuario, listAluguel[i].Usuario.TipoUsuario);
                Assert.Equal(listResult.ToList()[i].Usuario.CreateAt, listAluguel[i].Usuario.CreateAt); 
                Assert.Equal(listResult.ToList()[i].Usuario.UpdateAt, listAluguel[i].Usuario.UpdateAt);                                     
                Assert.Equal(listResult.ToList()[i].DataDevolucao, listAluguel[i].DataDevolucao); 
            }

            //Dto => Model
            var dtoCreate = new AluguelDto
            {
                UsuarioId = Guid.NewGuid(), 
                DataDevolucao = DateTime.UtcNow.AddHours(24.0)               
            };

            model = Mapper.Map<AluguelModel>(dtoCreate);
            Assert.NotNull(model);
            Assert.Equal(dtoCreate.UsuarioId, model.UsuarioId);
            Assert.Equal(dtoCreate.DataDevolucao, model.DataDevolucao);

            var dtoUpdate = new AluguelDtoUpdate
            {
                Id = Guid.NewGuid(),
                UsuarioId = Guid.NewGuid(), 
                DataDevolucao = DateTime.UtcNow.AddHours(24.0)               
            };

            model = Mapper.Map<AluguelModel>(dtoUpdate);
            Assert.NotNull(model);
            Assert.Equal(dtoUpdate.UsuarioId, model.UsuarioId);
            Assert.Equal(dtoUpdate.DataDevolucao, model.DataDevolucao);
        }        
    }
}