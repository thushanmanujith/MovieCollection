using Microsoft.EntityFrameworkCore;
using MovieCollection.Movie.Domain.Events.Handlers;
using MovieCollection.Movie.Domain.Infrastructure;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands.Handlers;
using MovieCollection.Movie.Domain.Ports.Incoming.Queries;
using MovieCollection.Movie.Domain.Ports.OutGoing;
using MovieCollection.Movie.Persistence;

namespace MovieCollection.WebAPI
{
    public static class MovieIocInstaller
    {
        public static void Install(IServiceCollection services, string databaseConnectionString)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            AddInterfaceImplementers(services, typeof(ICommandHandler<>));
            AddInterfaceImplementers(services, typeof(ICommandHandler<,>));

            services.AddScoped<IEventDispatcher, EventDispatcher>();
            AddInterfaceImplementers(services, typeof(IEventHandler<>));

            InstallPersistence(services, databaseConnectionString);
            services.AddScoped<IMoviePersistence, MoviePersistence>();
            services.AddScoped<IMovieQueries, MovieQueries>();
        }

        private static void InstallPersistence(IServiceCollection services, string databaseConnectionString)
        {
            services.AddDbContext<MovieDataContext>(options =>
            { options.UseNpgsql(databaseConnectionString); });

            services.AddControllers();
        }

        private static void AddInterfaceImplementers(IServiceCollection services, Type interfaceType)
        {
            var types = interfaceType.Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType));
            foreach (var type in types)
            {
                type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType).ToList().ForEach(i => services.AddScoped(i, type));
            }
        }
    }
}
