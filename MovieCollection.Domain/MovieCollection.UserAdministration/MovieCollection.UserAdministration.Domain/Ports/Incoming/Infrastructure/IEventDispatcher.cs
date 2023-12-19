using MovieCollection.UserAdministration.Domain.Events;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Infrastructure
{
    public interface IEventDispatcher
    {
        Task RaiseEvent<T>(T @event) where T : IEvent;
    }
}
