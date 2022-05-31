using API.Helpers.Extensions;
using API.Helpers.Middlewares;
using DAL.DataContext;
using DAL.Models.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Text;

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
                // Set up data directory
                AppDomain.CurrentDomain.SetData("DataDirectory", 
                    Path.Combine(Directory.GetCurrentDirectory(), "App_Data"));

                var appConfigration = new AppConfigration();

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddDbContext<DatabaseContext>();

                builder.Services.ConfigureCorsService();
                builder.Services.ConfigureDI();

                builder.Services.AddControllers()
                    .AddNewtonsoftJson(x =>
                        x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

                // NLog: Setup NLog for Dependency injection
                builder.Logging.ClearProviders();
                builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                builder.Host.UseNLog();

                builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                    });
                });

                builder.Services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        //ValidIssuer = Configuration["Jwt:Issuer"],
                        //ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appConfigration.SecretJwt))
                    };
                });

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

                ////// Who are you?
                //app.UseAuthentication();
                ////// Are you allowed to access?
                //app.UseAuthorization();
                app.UseMiddleware<JwtMiddleware>();


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