using Microsoft.Extensions.DependencyInjection;

namespace GrTechRK.DAL.Interfaces
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDALServices(this IServiceCollection services)
        {
            services.AddScoped<ICompanyDAL, CompanyDAL>();
            services.AddScoped<IEmployeeDAL, EmployeeDAL>();
        }
    }
}
