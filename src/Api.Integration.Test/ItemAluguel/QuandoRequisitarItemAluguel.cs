using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Dtos.Filme;
using src.Api.Domain.Dtos.Funcionario;
using src.Api.Domain.Dtos.ItemAluguel;
using src.Api.Domain.Dtos.Usuario;
using src.Api.Domain.Enuns;
using Xunit;

namespace src.Api.Integration.Test.ItemAluguel
{
    public class QuandoRequisitarItemAluguel : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Executar_Crud_ItemAluguel()
        {
            #region Moq
            var usuarioCliente = new UsuarioDto
            {
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            var response = await PostJsonAsync(usuarioCliente, $"{HostApi}usuarios", Client);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var cliente = JsonConvert.DeserializeObject<UsuarioDtoCreateResult>(jsonResult);

            var usuarioFuncionario = new FuncionarioDto
            {
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                Senha = "admin"
            };

            response = await PostJsonAsync(usuarioFuncionario, $"{HostApi}funcionarios", Client);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            jsonResult = await response.Content.ReadAsStringAsync();
            var funcionario = JsonConvert.DeserializeObject<FuncionarioDtoCreateResult>(jsonResult);

            var filmeDto = new FilmeDto
            {
                Titulo = Faker.Country.Name(),
                Categoria = (Categoria) new Random().Next(Enum.GetNames(typeof(Categoria)).Length),
                FuncionarioId = funcionario.Id
            };

            response = await PostJsonAsync(filmeDto, $"{HostApi}filmes", Client);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            jsonResult = await response.Content.ReadAsStringAsync();
            var filme1 = JsonConvert.DeserializeObject<FilmeDtoCreateResult>(jsonResult);


            filmeDto = new FilmeDto
            {
                Titulo = Faker.Country.Name(),
                Categoria = (Categoria) new Random().Next(Enum.GetNames(typeof(Categoria)).Length),
                FuncionarioId = funcionario.Id
            };

            response = await PostJsonAsync(filmeDto, $"{HostApi}filmes", Client);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            jsonResult = await response.Content.ReadAsStringAsync();
            var filme2 = JsonConvert.DeserializeObject<FilmeDtoCreateResult>(jsonResult);

            var aluguelDto = new AluguelDto
            {
                UsuarioId = cliente.Id,
                DataDevolucao = default(DateTime)
            };

            response = await PostJsonAsync(aluguelDto, $"{HostApi}alugueis", Client);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            jsonResult = await response.Content.ReadAsStringAsync();
            var aluguel = JsonConvert.DeserializeObject<AluguelDtoCreateResult>(jsonResult); 

            var item1 = new ItemAluguelDto
            {
                AluguelId = aluguel.Id,
                FilmeId = filme1.Id,
            };

            var item2 = new ItemAluguelDto
            {
                AluguelId = aluguel.Id,
                FilmeId = filme2.Id
            };
            #endregion

            #region Post
            response = await PostJsonAsync(item1, $"{HostApi}itensAlugueis", Client);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            jsonResult = await response.Content.ReadAsStringAsync();
            var postItem = JsonConvert.DeserializeObject<ItemAluguelDtoCreateResult>(jsonResult);
            Assert.NotNull(postItem);
            Assert.True(postItem.CreateAt != default(DateTime));
            Assert.True(postItem.Id != default(Guid));
            Assert.Equal(postItem.AluguelId, aluguel.Id);
            Assert.Equal(postItem.FilmeId, filme1.Id);
            #endregion

            #region Put
            var itemUpdate = new ItemAluguelDtoUpdate
            {
                Id = postItem.Id,
                FilmeId = filme2.Id,
                AluguelId = aluguel.Id
            };

            response = await Client.PutAsync($"{HostApi}itensAlugueis", 
                                             new StringContent(JsonConvert.SerializeObject(itemUpdate),
                                             Encoding.UTF8, "application/json"));
            jsonResult = await response.Content.ReadAsStringAsync();
            var putResult = JsonConvert.DeserializeObject<ItemAluguelDtoUpdateResult>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.Equal(putResult.FilmeId, filme2.Id);
            Assert.Equal(putResult.Id, postItem.Id);
            Assert.True(putResult.UpdateAt != default(DateTime));
            #endregion

            #region GetAllItensByAluguelId
            response = await Client.GetAsync($"{HostApi}itensAlugueis/GetAllItensByAluguelId/{aluguel.Id}");
            jsonResult = await response.Content.ReadAsStringAsync();
            var getResult = JsonConvert.DeserializeObject<IEnumerable<ItemAluguelDtoGetResult>>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(getResult.Count() > 0);
            Assert.True(getResult.Where(i => i.Id == postItem.Id).Count() == 1);
            Assert.True(getResult.Where(i => i.Filme.Id == filme2.Id).Count() == 1);
            #endregion

            #region Delete
            response = await Client.DeleteAsync($"{HostApi}itensAlugueis/{postItem.Id}");
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<bool>(jsonResult);
            Assert.True(result);

            //Get Confirmação Delete
            response = await Client.GetAsync($"{HostApi}itensAlugueis/GetAllItensByAluguelId/{aluguel.Id}");
            jsonResult = await response.Content.ReadAsStringAsync();
            getResult = JsonConvert.DeserializeObject<IEnumerable<ItemAluguelDtoGetResult>>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.False(getResult.Where(i => i.Id == postItem.Id).Count() == 1);
            #endregion
           
        }
    }
}