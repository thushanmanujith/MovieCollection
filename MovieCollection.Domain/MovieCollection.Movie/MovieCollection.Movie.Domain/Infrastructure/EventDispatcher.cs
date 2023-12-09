using Microsoft.Extensions.DependencyInjection;
using MovieCollection.Movie.Domain.Events;
using MovieCollection.Movie.Domain.Events.Handlers;

namespace MovieCollection.Movie.Domain.Infrastructure
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task RaiseEvent<T>(T @event) where T : IEvent
        {
            var handlers = _serviceProvider.GetServices<IEventHandler<T>>().ToList();
            if (!handlers.Any())
                return;

            foreach (var handler in handlers)
                await handler.Handle(@event);
        }
    }
}
