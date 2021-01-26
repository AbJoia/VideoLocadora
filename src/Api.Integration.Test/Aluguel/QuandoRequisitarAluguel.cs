using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using src.Api.Domain.Dtos.Aluguel;
using src.Api.Domain.Dtos.Usuario;
using Xunit;

namespace src.Api.Integration.Test.Aluguel
{
    public class QuandoRequisitarAluguel : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Executar_Crud_Aluguel()
        {
            
            #region Moq
            var usuario = new UsuarioDto
            {
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            var usuarioResponse = await PostJsonAsync(usuario, $"{HostApi}usuarios", Client);
            Assert.True(usuarioResponse.StatusCode == HttpStatusCode.Created);
            var jsonResult = await usuarioResponse.Content.ReadAsStringAsync();
            var usuarioResult = JsonConvert.DeserializeObject<UsuarioDtoCreateResult>(jsonResult);

            var usuarioUpdate = new UsuarioDto
            {
                Nome = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };            

            var usuarioResponseUpdate = await PostJsonAsync(usuarioUpdate, $"{HostApi}usuarios", Client);
            Assert.True(usuarioResponseUpdate.StatusCode == HttpStatusCode.Created);
            jsonResult = await usuarioResponseUpdate.Content.ReadAsStringAsync();
            var usuarioResultUpdate = JsonConvert.DeserializeObject<UsuarioDtoCreateResult>(jsonResult);

            var aluguelDto = new AluguelDto
            {
                UsuarioId = usuarioResult.Id,
                DataDevolucao = DateTime.UtcNow.AddHours(72.0)
            };
            #endregion

            #region Post
            var response = await PostJsonAsync(aluguelDto, $"{HostApi}alugueis", Client); 
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            jsonResult = await response.Content.ReadAsStringAsync();
            var postResult = JsonConvert.DeserializeObject<AluguelDtoCreateResult>(jsonResult);
            Assert.NotNull(postResult);
            Assert.True(postResult.Id != default(Guid));            
            Assert.Equal(postResult.UsuarioId, usuarioResult.Id);
            Assert.True(postResult.CreateAt != default(DateTime));
            Assert.True(postResult.CreateAt.CompareTo(postResult.DataDevolucao) < 0 );
            #endregion 

            #region Put
            var aluguelUpdate = new AluguelDtoUpdate
            {
                Id = postResult.Id,
                DataDevolucao = postResult.DataDevolucao.AddHours(72.0),
                UsuarioId = usuarioResultUpdate.Id,
            };
            
            response = await Client.PutAsync($"{HostApi}alugueis",
                                    new StringContent(JsonConvert.SerializeObject(aluguelUpdate),
                                    Encoding.UTF8,
                                    "application/json"));
            jsonResult = await response.Content.ReadAsStringAsync();
            var putResult = JsonConvert.DeserializeObject<AluguelDtoUpdateResult>(jsonResult); 
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.Equal(putResult.Id, postResult.Id);
            Assert.NotEqual(putResult.UsuarioId, aluguelDto.UsuarioId);
            Assert.True(putResult.UpdateAt != default(DateTime));
            Assert.True(putResult.UpdateAt.CompareTo(postResult.CreateAt) > 0);
            Assert.True(putResult.DataDevolucao.CompareTo(aluguelDto.DataDevolucao) > 0);
            #endregion 

            #region GetAllByUsuarioId
            response = await Client.GetAsync($"{HostApi}alugueis/GetAllByUsuarioId/{usuarioResultUpdate.Id}");
            jsonResult = await response.Content.ReadAsStringAsync();
            var getAllResult = JsonConvert
                               .DeserializeObject<IEnumerable<AluguelDtoGetResult>>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(getAllResult.Count() > 0);
            Assert.True(getAllResult.Where(a => a.Id == postResult.Id).Count() == 1);
            Assert.True(getAllResult.FirstOrDefault(a => 
                        a.Usuario.Id == putResult.UsuarioId)
                        .Usuario.Nome == usuarioUpdate.Nome);
            #endregion 

            #region GetCompleteById
            response = await Client.GetAsync($"{HostApi}alugueis/GetCompleteById/{postResult.Id}");
            jsonResult = await response.Content.ReadAsStringAsync();
            var getComplete = JsonConvert.DeserializeObject<AluguelDtoCompleteResult>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.Equal(getComplete.Id, postResult.Id);            
            Assert.NotNull(getComplete.Usuario);
            Assert.Equal(getComplete.Usuario.Id, usuarioResultUpdate.Id);
            Assert.Equal(getComplete.Usuario.Nome, usuarioResultUpdate.Nome);
            Assert.Equal(getComplete.Usuario.Email, usuarioResultUpdate.Email);                       
            Assert.NotNull(getComplete.ItensAluguel);
            #endregion       

            #region Delete
            response = await Client.DeleteAsync($"{HostApi}alugueis/{postResult.Id}");            
            jsonResult = await response.Content.ReadAsStringAsync();
            var deleteResult = JsonConvert.DeserializeObject<bool>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True(deleteResult);

            //Get Confirmação Delete
            response = await Client.GetAsync($"{HostApi}alugueis/GetCompleteById/{postResult.Id}");
            jsonResult = await response.Content.ReadAsStringAsync();
            getComplete = JsonConvert.DeserializeObject<AluguelDtoCompleteResult>(jsonResult);
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
            #endregion
           
        }        
    }
}