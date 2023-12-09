using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using MovieCollection.UserAdministration.Domain.Infrastructure;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Handlers;
using MovieCollection.UserAdministration.Domain.Ports.OutGoing;
using NUnit.Framework;

namespace MovieCollection.UserAdministration.Test
{
    [SetUpFixture]
    public class TestBase
    {
        protected IHost Host;
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
                    services.AddScoped(_ => userAdministrationPersistenceMock.Object);
                    services.AddScoped<ICommandDispatcher, CommandDispatcher>();
                    AddInterfaceImplementers(services, typeof(ICommandHandler<>));
                    AddInterfaceImplementers(services, typeof(ICommandHandler<,>));
                })
                .ConfigureHostConfiguration(config => new ConfigurationBuilder())
                .Build();

            commandDispatcher = Host.Services.GetService(typeof(ICommandDispatcher)) as ICommandDispatcher;
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