using EmployeeSystemMangement.BLL.Interfaces;
using EmployeeSystemMangement.BLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSystemMangement.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServicesExtensions(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
