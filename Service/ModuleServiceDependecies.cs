using Data.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Service.Abstruct;
using Service.AuthService.Impelemention;
using Service.AuthService.InterFace;
using Service.Impelmention;
using System.Collections.Concurrent;

namespace Service
{
    public static class ModuleServiceDependecies
    {
        public static IServiceCollection AddServiceDependecies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizService, AuthorizService>();
            services.AddTransient<IEmailsService, EmailsService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IInstractorService, InstractorService>();

            services.AddTransient<IFileService, FileService>();






            services.AddSingleton<ConcurrentDictionary<string, RefreshToken>>();



            return services;
        }

    }
}
