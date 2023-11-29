using Microsoft.EntityFrameworkCore;
using ToDoList.Data;

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
            builder.Services.AddDbContext<ToDoListContext>(options =>
                options.UseSqlServer(@"Data Source=DESKTOP-PPF02FT\SQLEXPRESS;Initial Catalog=ToDoList;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
                       .EnableDetailedErrors()
                       .EnableSensitiveDataLogging()
                       .LogTo(
                           Console.WriteLine,
                           new[] { DbLoggerCategory.Database.Command.Name },
                           LogLevel.Information));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.Run();
        }
    }
}