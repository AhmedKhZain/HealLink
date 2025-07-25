using HealLink.Application;
using HealLink.Infrastructure;
using HealLink.Presentation;
namespace HealLink.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);




            // Add services to the container.

            builder.Services
                .AddApplication()
                .AddInfrastructure(builder.Configuration)
                .AddPresentation();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
