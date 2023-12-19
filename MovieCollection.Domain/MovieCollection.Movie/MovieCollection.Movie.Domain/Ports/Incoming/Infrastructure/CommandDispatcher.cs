using MovieCollection.Movie.Domain.Ports.Incoming.Commands;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace MovieCollection.Movie.Domain.Ports.Incoming.Infrastructure
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Dispatch<T>(T command) where T : ICommand
        {
            var handler = _serviceProvider.GetService<ICommandHandler<T>>();
            if (handler == null)
            {
                throw new ApplicationException($"No Commandhandler registered for handling {typeof(T)}");
            }
            await handler.Handle(command);
        }

        public async Task<TResult> Dispatch<TCommand, TResult>(TCommand command) where TCommand : ICommand
        {
            var handler = _serviceProvider.GetService<ICommandHandler<TCommand, TResult>>();
            if (handler == null)
            {
                throw new ApplicationException($"No Commandhandler registered for handling {typeof(TCommand)}");
            }
            return await handler.Handle(command);
        }
    }
}
