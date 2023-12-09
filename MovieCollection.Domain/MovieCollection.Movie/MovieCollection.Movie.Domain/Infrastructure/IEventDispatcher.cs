using MovieCollection.Movie.Domain.Events;

namespace MovieCollection.Movie.Domain.Infrastructure
{
    public interface IEventDispatcher
    {
        Task RaiseEvent<T>(T @event) where T : IEvent;
    }
}
