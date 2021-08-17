using GrTechRK.BSL.Common;
using Microsoft.Extensions.DependencyInjection;

namespace GrTechRK.BSL.Interfaces
{
    public static class IServiceCollectionExtensions
    {
        public static void AddBSLServices(this IServiceCollection services)
        {
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddSingleton<IMailService, MailService>();
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
