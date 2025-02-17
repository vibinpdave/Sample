using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HRMS.Application
{
    #region Class-ApplicationServiceRegistration
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
    #endregion
}
