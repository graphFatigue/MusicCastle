using bestill.DAL.Interfaces;
using bestill.DAL.Repositories;
using bestill.Domain.Entity;
using bestill.Service.Implementations;
using bestill.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace bestill
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Artist>, ArtistRepository>();
            //services.AddScoped<IBaseRepository<User>, UserRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IArtistService, ArtistService>();
            //services.AddScoped<IAccountService, AccountService>();
        }
    }
}
