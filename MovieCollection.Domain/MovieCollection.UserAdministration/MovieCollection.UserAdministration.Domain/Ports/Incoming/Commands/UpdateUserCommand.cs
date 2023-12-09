namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands
{
    public class UpdateUserCommand : ICommand
    {
        public UpdateUserCommand(int id, string email, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; set; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
