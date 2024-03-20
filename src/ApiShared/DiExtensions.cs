using ApiShared.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace ApiShared
{
    public static class DiExtensions
    {
        public static IServiceCollection AddApiShared(
            this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ResultsProfile));

            return services;
        }
    }
}
