namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task Handle(T command);
    }

    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand
    {
        Task<TResult> Handle(TCommand command);
    }
}
