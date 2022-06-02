using DAL.Entities.Login;
using DAL.Entities.Store;
using DAL.Models.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using RestSharp;
using System.Text;
using WEB.Helpers.Middlewares;
using WEB.Helpers.Services;
using WEB.Helpers.Services.Base;

namespace WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            try
            {
                var appConfigration = new AppConfigration();

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                builder.Services.AddControllersWithViews();
                builder.Services.AddSession();
                builder.Services.AddCookiePolicy(options =>
                {
                    // Set the secure flag, which Chrome's changes will require for SameSite none.
                    // Note this will also require you to be running on HTTPS
                    options.Secure = CookieSecurePolicy.SameAsRequest;

                    // Set the cookie to HTTP only which is good practice unless you really do need
                    // to access it client side in scripts.
                    options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;

                    // Add the SameSite attribute, this will emit the attribute with a value of none.
                    // To not emit the attribute at all set the SameSite property to SameSiteMode.Unspecified.
                    options.MinimumSameSitePolicy = SameSiteMode.Strict;
                });

                // NLog: Setup NLog for Dependency injection
                builder.Logging.ClearProviders();
                builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                builder.Host.UseNLog();

                builder.Services.AddSingleton<RestClient>();
                builder.Services.AddSingleton<IRestClientServiceProvider, RestClientServiceProvider>();
                builder.Services.AddSingleton<IService<User>, UserService>();
                builder.Services.AddSingleton<IService<Marka>, MarkaService>();
                builder.Services.AddSingleton<IService<Product>, ProductService>();

                builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

                builder.Services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/User/Login";
                        options.LogoutPath = "/User/Logout";
                        options.AccessDeniedPath = "/User/AccessDenied";
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
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseStatusCodePagesWithReExecute("/Errors/{0}");

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseSession();
                app.UseCookiePolicy();

                ////// Who are you?
                //app.UseAuthentication();
                ////// Are you allowed to access?
                //app.UseAuthorization();
                app.UseMiddleware<JwtMiddleware>();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Users}/{action=RegisterLogin}/{id?}");
                    //pattern: "{controller=Home}/{action=Index}/{id?}");

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