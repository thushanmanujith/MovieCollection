using MovieCollection.UserAdministration.Domain.Ports.Incoming.Events;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Events.Handlers
{
    public interface IEventHandler<T> where T : IEvent
    {
        Task Handle(T @event);
    }
}
