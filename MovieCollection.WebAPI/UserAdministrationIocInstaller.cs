using Microsoft.EntityFrameworkCore;
using MovieCollection.UserAdministration.Domain.Events.Handlers;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Handlers;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Infrastructure;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Queries;
using MovieCollection.UserAdministration.Domain.Ports.OutGoing;
using MovieCollection.UserAdministration.Domain.Settings;
using MovieCollection.UserAdministration.Domain.Utility;
using MovieCollection.UserAdministration.Persistence;

namespace MovieCollection.WebAPI
{
    public static class UserAdministrationIocInstaller
    {
        public static void Install(IServiceCollection services, ConfigurationManager configurationManager, string databaseConnectionString)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            AddInterfaceImplementers(services, typeof(ICommandHandler<>));
            AddInterfaceImplementers(services, typeof(ICommandHandler<,>));

            services.AddScoped<IEventDispatcher, EventDispatcher>();
            AddInterfaceImplementers(services, typeof(IEventHandler<>));

            InstallPersistence(services, databaseConnectionString);
            services.AddScoped<IUserAdministrationPersistence, UserAdministrationPersistence>();
            services.AddSingleton(configurationManager.GetSection(nameof(JwtSettings)).Get<JwtSettings>());
            services.AddSingleton<TokenFactory>();
            services.AddScoped<IUserQueries, UserQueries>();
        }

        private static void InstallPersistence(IServiceCollection services, string databaseConnectionString)
        {
            services.AddDbContext<UserAdministrationDataContext>(options => 
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
