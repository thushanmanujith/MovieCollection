using MovieCollection.UserAdministration.Domain.Enums;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands
{
    public class GrantAccessCommand : ICommand
    {
        public GrantAccessCommand(int userId, UserRole userRole)
        {
            UserRole = userRole;
            UserId = userId;
        }

        public int UserId { get; }
        public UserRole UserRole { get; }
    }
}
