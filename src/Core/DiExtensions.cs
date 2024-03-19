using Core.BogusRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DiExtensions
    {
        public static IServiceCollection AddDemoCore(
            this IServiceCollection services)
        {
            services.AddScoped<PersonsService>();
            services.AddSingleton<Storage>();
            services.AddSingleton<IPersonsRepository, PersonsRepository>();
            services.AddSingleton<INotesRepository, NotesRepository>();

            return services;
        }
    }
}
