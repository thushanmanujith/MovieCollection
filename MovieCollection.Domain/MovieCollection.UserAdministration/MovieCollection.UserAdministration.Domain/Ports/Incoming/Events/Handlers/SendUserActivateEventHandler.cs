using MovieCollection.UserAdministration.Domain.Ports.Incoming.Events;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Events.Handlers
{
    public class SendUserActivateEventHandler : IEventHandler<SendUserActivateEvent>
    {
        public Task Handle(SendUserActivateEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
