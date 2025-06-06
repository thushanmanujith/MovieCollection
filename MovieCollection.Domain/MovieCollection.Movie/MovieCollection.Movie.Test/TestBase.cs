﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using MovieCollection.Movie.Domain.Ports.Incoming.Infrastructure;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands.Handlers;
using MovieCollection.Movie.Domain.Ports.OutGoing;
using NUnit.Framework;

namespace MovieCollection.Movie.Test
{
    public class TestBase
    {
        protected IHost Host;
        protected IServiceScope Scope;
        protected ICommandDispatcher commandDispatcher;
        protected Mock<IMoviePersistence> moviePersistenceMock = new Mock<IMoviePersistence>();

        [OneTimeSetUp]
        public void SetUpBase()
        {
            Host = new HostBuilder()
                .UseEnvironment("Development")
                .ConfigureHostConfiguration(config => new ConfigurationBuilder())
                .ConfigureServices(services =>
                {
                    services.AddScoped(_ => moviePersistenceMock.Object);
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