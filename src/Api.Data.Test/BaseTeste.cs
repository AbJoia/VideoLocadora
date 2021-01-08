using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using src.Api.Data.Context;

namespace src.Api.Data.Test
{
    public class BaseTeste
    {
        public BaseTeste()
        {            
        }
    }

    public class DbTest : IDisposable
    {
        private string DbName = "api-videolocadora-db-test_"
                                + Guid.NewGuid().ToString().Replace("-", string.Empty);
        public ServiceProvider serviceProvider {get; private set;}

        public DbTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(options =>
                options.UseMySql("Server=localhost;"
                                 +"Port=3306;"
                                 +$"Database={DbName};"
                                 +"Uid=root;"
                                 +"Pwd=admin123"),
                                 ServiceLifetime.Transient                    
            );

            serviceProvider = serviceCollection.BuildServiceProvider();

            using(var context = serviceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using(var context = serviceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}