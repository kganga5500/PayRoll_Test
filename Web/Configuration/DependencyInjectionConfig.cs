using PaylocityProduct.Domain.Interfaces;
using PaylocityProduct.Domain.Services;
using PaylocityProduct.Infrastructure.Context;
using PaylocityProduct.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace PaylocityProduct.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<PayrollDBContext>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IDependentRepository, DependentRepository>();

            services.AddScoped<IDependentService, DependentService>();
            services.AddScoped<IDeductionCalculator, DeductionCalculator>();


            return services;
        }
    }
}