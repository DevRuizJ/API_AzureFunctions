using Application.Common;
using Application.Interface.Security.Jwt;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
//using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Security.Jwt;
using Security.Jwt.Validation;

namespace Security
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IJwtValidation, JwtValidation>();

            return services;
        }
    }
}