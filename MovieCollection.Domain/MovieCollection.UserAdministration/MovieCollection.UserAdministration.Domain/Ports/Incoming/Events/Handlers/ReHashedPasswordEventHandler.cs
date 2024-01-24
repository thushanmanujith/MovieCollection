using Microsoft.AspNetCore.Identity;
using MovieCollection.UserAdministration.Domain.Ports.OutGoing;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Events.Handlers
{
    public class ReHashedPasswordEventHandler : IEventHandler<ReHashedPasswordEvent>
    {
        private IUserAdministrationPersistence _userAdministrationPersistence;

        public ReHashedPasswordEventHandler(IUserAdministrationPersistence userAdministrationPersistence) 
        {
            _userAdministrationPersistence = userAdministrationPersistence;
        }

        public async Task Handle(ReHashedPasswordEvent @event)
        {
            var hasher = new PasswordHasher<string>();
            var reHashedPassword = hasher.HashPassword(@event.User.Email, @event.Password);
            @event.User.UpdateUserPassword(reHashedPassword);
            await _userAdministrationPersistence.AddUpdateUserAsync(@event.User);
        }
    }
}
