namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands
{
    public class AuthenticateCommand : ICommand
    {
        public AuthenticateCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
