using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Application;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using src.Api.CrossCutting.Mappings;
using src.Api.Data.Context;

namespace src.Api.Integration.Test
{
    public abstract class BaseIntegration : IDisposable
    {
        public MyContext MyContext { get; set; }
        public HttpClient Client {get; set; }
        public IMapper Mapper { get; set; }
        public string HostApi { get; set; }
        public HttpResponseMessage Response { get; set; }

        public BaseIntegration()
        { 
            HostApi = "http://localhost:5000/video-locadora/";           
            var builder = new WebHostBuilder()
                        .UseEnvironment("IntegrationTest")
                        .UseStartup<Startup>();    
            var server = new TestServer(builder);

            MyContext = server.Host.Services            
                        .GetService(typeof(MyContext)) as MyContext;            
            MyContext.Database.Migrate();

            Mapper = new AutoMapperFixture().GetMapper();

            Client = server.CreateClient();
        }

        public static async Task<HttpResponseMessage> PostJsonAsync(
                        object dataClass, string url, HttpClient client)
        {
            return await client.PostAsync(url, 
                        new StringContent(JsonConvert.SerializeObject(dataClass), 
                        Encoding.UTF8, "application/json"));
        }

        public void Dispose()
        {
            MyContext.Dispose();
            Client.Dispose();
        }
    }

    public class AutoMapperFixture : IDisposable
    {
        public IMapper GetMapper()
        {
            var config = new AutoMapper.MapperConfiguration(cfg => {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });
            return config.CreateMapper();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}