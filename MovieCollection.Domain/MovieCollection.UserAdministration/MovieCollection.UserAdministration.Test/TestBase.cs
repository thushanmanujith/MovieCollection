using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Handlers;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Infrastructure;
using MovieCollection.UserAdministration.Domain.Ports.OutGoing;
using MovieCollection.UserAdministration.Domain.Settings;
using MovieCollection.UserAdministration.Domain.Utility;
using NUnit.Framework;

namespace MovieCollection.UserAdministration.Test
{
    public class TestBase
    {
        protected IHost Host;
        protected IServiceScope Scope;
        protected ICommandDispatcher commandDispatcher;
        protected Mock<IUserAdministrationPersistence> userAdministrationPersistenceMock = new Mock<IUserAdministrationPersistence>();

        [OneTimeSetUp]
        public void SetUpBase()
        {
            Host = new HostBuilder()
                .UseEnvironment("Development")
                .ConfigureHostConfiguration(config => new ConfigurationBuilder())
                .ConfigureServices(services =>
                {
                    var jwtSettings = new JwtSettings
                    {
                        Secret = "test-secret-key",
                        Issuer = "test-issuer",
                    };
                    services.AddSingleton(jwtSettings);
                    services.AddSingleton<TokenFactory>();

                    var eventDispatcherMock = new Mock<IEventDispatcher>();
                    services.AddScoped(_ => eventDispatcherMock.Object);

                    services.AddScoped(_ => userAdministrationPersistenceMock.Object);
                    services.AddScoped<ICommandDispatcher, CommandDispatcher>();
                    AddInterfaceImplementers(services, typeof(ICommandHandler<>));
                    AddInterfaceImplementers(services, typeof(ICommandHandler<,>));
                })
                .ConfigureHostConfiguration(config => new ConfigurationBuilder())
                .Build();
        }

        [SetUp]
        public void CreateScope()
        {
            Scope = Host.Services.CreateScope();
            commandDispatcher = Scope.ServiceProvider.GetRequiredService<ICommandDispatcher>();
        }

        [TearDown]
        public void DisposeScope()
        {
            Scope.Dispose();
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