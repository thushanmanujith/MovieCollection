namespace MovieCollection.UserAdministration.Domain.DTOs
{
    public class UserEntityDto
    {
        public UserEntityDto(int id, string email, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
