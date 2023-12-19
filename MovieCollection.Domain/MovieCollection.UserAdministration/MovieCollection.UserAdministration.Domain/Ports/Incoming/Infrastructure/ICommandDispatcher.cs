using MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Infrastructure
{
    public interface ICommandDispatcher
    {
        Task Dispatch<T>(T command) where T : ICommand;
        Task<TResult> Dispatch<TCommand, TResult>(TCommand command) where TCommand : ICommand;
    }
}
