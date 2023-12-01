using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;

namespace ToDoListIntegrationTest
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IDisposable where TProgram : class
    {
        public DbContextOptions<ToDoListItemContext> Options { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                   d => d.ServiceType ==
                       typeof(DbContextOptions<ToDoListItemContext>));

                services.Remove(dbContextDescriptor);

                string connectionString = @"Data Source=DESKTOP-PPF02FT\SQLEXPRESS;Initial Catalog=ToDoListTestDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

                var optionsBuilder = new DbContextOptionsBuilder<ToDoListItemContext>();
                optionsBuilder.UseSqlServer(connectionString);
                services.AddScoped<DbContextOptions<ToDoListItemContext>>((pr) => optionsBuilder.Options);

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
            ToDoListItemContext context = new ToDoListItemContext(Options);
            context.Database.EnsureDeleted();
            base.Dispose();
        }
    }
}
