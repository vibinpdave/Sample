namespace HRMS.API.Extensions
{
    public static class CORSExtension
    {
        public static void AddCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyHeader().AllowAnyMethod()
                    .WithOrigins("http://localhost:4200", "https://localhost:4200")
                    .Build());
            });
        }
    }
}
