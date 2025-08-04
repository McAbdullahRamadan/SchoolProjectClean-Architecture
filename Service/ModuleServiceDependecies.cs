using Microsoft.Extensions.DependencyInjection;
using Service.Abstruct;
using Service.Impelmention;

namespace Service
{
    public static class ModuleServiceDependecies
    {
        public static IServiceCollection AddServiceDependecies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();


            return services;
        }

    }
}
