using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;

namespace ToDoListIntegrationTest
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IDisposable where TProgram : class
    {
        public DbContextOptions<ToDoListContext> Options { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                   d => d.ServiceType ==
                       typeof(DbContextOptions<ToDoListContext>));

                services.Remove(dbContextDescriptor);

                string connectionString = @"Data Source=DESKTOP-PPF02FT\SQLEXPRESS;Initial Catalog=ToDoListTestDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

                var optionsBuilder = new DbContextOptionsBuilder<ToDoListContext>();
                optionsBuilder.UseSqlServer(connectionString);
                services.AddScoped<DbContextOptions<ToDoListContext>>((pr) => optionsBuilder.Options);

                Options = optionsBuilder.Options;
            });
        }
    }

    public class DatabaseFixture<T> : CustomWebApplicationFactory<T>, IDisposable where T : class
    {
        public DatabaseFixture()
        {
        }

        public void Dispose()
        {
            ToDoListContext context = new ToDoListContext(Options);
            context.Database.EnsureDeleted();
            base.Dispose();
        }
    }
}
