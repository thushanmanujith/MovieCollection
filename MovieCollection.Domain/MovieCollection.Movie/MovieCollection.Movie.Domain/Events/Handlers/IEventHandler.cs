namespace MovieCollection.Movie.Domain.Events.Handlers
{
    public interface IEventHandler<T> where T : IEvent
    {
        Task Handle(T @event);
    }
}
