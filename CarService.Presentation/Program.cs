
using CarService.Application.Services;
using CarService.Application.Settings;
using CarService.DAL.Infrastructure;
using CarService.DAL.Interface;
using CarService.DAL.Repositories;
using CarService.Domain.Interfaces;
using Serilog;

namespace CarService.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Настройка Serilog из конфигурации
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection(AppSettings.SectionName));

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<CarService.Application.Services.CarService>();


            var app = builder.Build();
            try
            {
                Log.Information("Запуск приложения {AppName} версии {AppVersion}",
                    builder.Configuration["AppSettings:AppName"],
                    builder.Configuration["AppSettings:AppVersion"]);
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();
                Log.Information("Приложение настроено и готово к работе на порту {Port}",
    builder.Configuration["urls"] ?? "default");
                Console.WriteLine("Тест");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Критическая ошибка при запуске приложения");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}
