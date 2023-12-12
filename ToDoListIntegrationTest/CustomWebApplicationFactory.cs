using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;

namespace ToDoListIntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IDisposable where TProgram : class
    {
        public DbContextOptions<ToDoListItemContext> Options { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                ServiceDescriptor? dbContextDescriptor = services.SingleOrDefault(
                   d => d.ServiceType ==
                       typeof(DbContextOptions<ToDoListItemContext>));
                services.Remove(dbContextDescriptor);

                IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
                string connection = configuration["ConnectionStringTest"];

                DbContextOptionsBuilder<ToDoListItemContext> optionsBuilder = new DbContextOptionsBuilder<ToDoListItemContext>();
                optionsBuilder.UseSqlServer(connection);
                services.AddScoped<DbContextOptions<ToDoListItemContext>>((pr) => optionsBuilder.Options);

                Options = optionsBuilder.Options;
            });
        }
    }

    public class DatabaseFixture<T> : CustomWebApplicationFactory<T>, IDisposable where T : class
    {
        public void Dispose()
        {
            ToDoListItemContext context = new ToDoListItemContext(Options);
            context.Database.EnsureDeleted();
            base.Dispose();
        }
    }
}
