using MovieCollection.UserAdministration.Domain.Entities;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Events
{
    public class ReHashedPasswordEvent : IEvent
    {
        public ReHashedPasswordEvent(User user, string password)
        {
            User = user;
            Password = password;
        }

        public User User { get; }
        public string Password { get; }
    }
}
