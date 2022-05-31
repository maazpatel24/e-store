using API.Helpers.Extensions;
using DAL.DataContext;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddDbContext<DatabaseContext>();

                builder.Services.ConfigureCorsService();
                builder.Services.ConfigureDI();

                builder.Services.AddControllers();

                // NLog: Setup NLog for Dependency injection
                builder.Logging.ClearProviders();
                builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                builder.Host.UseNLog();

                builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.ConfigureCustomExceptionMiddleware();

                app.UseHttpsRedirection();

                app.UseRouting();
                app.UseCorsService();

                //// Who are you?
                app.UseAuthentication();
                //// Are you allowed to access?
                app.UseAuthorization();

                app.MapControllers();

                // migrate any database changes on startup (includes initial db creation)
                using (var scope = app.Services.CreateScope())
                {
                    var dataContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    dataContext.Database.Migrate();
                }

                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }
    }
}