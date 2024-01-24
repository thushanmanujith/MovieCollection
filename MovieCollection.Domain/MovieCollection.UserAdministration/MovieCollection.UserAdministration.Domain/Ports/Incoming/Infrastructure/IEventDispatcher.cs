using MovieCollection.UserAdministration.Domain.Ports.Incoming.Events;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Infrastructure
{
    public interface IEventDispatcher
    {
        Task RaiseEventAsync<T>(T @event) where T : IEvent;
    }
}
