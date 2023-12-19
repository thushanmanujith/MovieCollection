using MovieCollection.Movie.Domain.Ports.Incoming.Commands;

namespace MovieCollection.Movie.Domain.Ports.Incoming.Infrastructure
{
    public interface ICommandDispatcher
    {
        Task Dispatch<T>(T command) where T : ICommand;
        Task<TResult> Dispatch<TCommand, TResult>(TCommand command) where TCommand : ICommand;
    }
}
