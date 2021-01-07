using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace src.Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connection = "Server=localhost;"
                            +"Port=3306;"
                            +"Database=video_locadora_api;"
                            +"Uid=root;"
                            +"Pwd=admin123";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(connection);
            return new MyContext(optionsBuilder.Options);
        }
    }
}