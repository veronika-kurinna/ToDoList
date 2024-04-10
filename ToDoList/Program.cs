using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Data.Repositories;
using ToDoList.Services;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddDbContext<ToDoListItemContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionString"])
                       .EnableDetailedErrors()
                       .EnableSensitiveDataLogging()
                       .LogTo(
                           Console.WriteLine,
                           new[] { DbLoggerCategory.Database.Command.Name },
                           LogLevel.Information));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IToDoListItemService, ToDoListItemService>();
            builder.Services.AddTransient<IToDoListItemRepository, ToDoListItemRepository>();
            builder.Services.AddTransient<IToDoListItemMapper, ToDoListItemMapper>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ToDoListItemContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}