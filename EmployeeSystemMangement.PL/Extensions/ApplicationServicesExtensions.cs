using EmployeeSystemMangement.BLL.Interfaces;
using EmployeeSystemMangement.BLL.Repositories;
using EmployeeSystemMangement.PL.Services.SendEmail;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSystemMangement.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServicesExtensions(this IServiceCollection services)
        {
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISendEmail, SendEmail>();
        }
    }
}
