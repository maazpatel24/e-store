namespace API.Helpers.Extensions
{
    public static class CorsExtensions
    {
        public static void ConfigureCorsService(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void UseCorsService(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
        }
    }
}