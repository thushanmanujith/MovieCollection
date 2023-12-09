using MovieCollection.UserAdministration.Domain.Enums;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands
{
    public class RegisterUserCommand : ICommand
    {
        public RegisterUserCommand(string email, string password, string firstName, string lastName)
        {
            Password = password;
            Email = email;
            UserRole = UserRole.User;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Email { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public UserRole UserRole { get; }
    }
}
