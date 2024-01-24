using MovieCollection.UserAdministration.Domain.Enums;

namespace MovieCollection.UserAdministration.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole UserRole { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public User() { }
        public User(string email, string hashedPassword, string firstName, string lastName, UserRole userRole)
        {
            Email = email.ToLowerInvariant();
            HashedPassword = hashedPassword;
            FirstName = firstName;
            LastName = lastName;
            UserRole = userRole;
            IsActive = false;
            CreatedOn = DateTime.Now;
        }

        public User(int id, string email, string hashedPassword, string firstName, string lastName, UserRole userRole)
        {
            Id = id;
            Email = email.ToLowerInvariant();
            HashedPassword = hashedPassword;
            FirstName = firstName;
            LastName = lastName;
            UserRole = userRole;
            IsActive = false;
            CreatedOn = DateTime.Now;
        }

        public void SetUserRole(UserRole userRole)
        {
            UserRole = userRole;
        }

        public void UpdateUser(string email, string firstName, string lastName)
        {
            Email = email.ToLowerInvariant();
            FirstName = firstName;
            LastName = lastName;
        }

        public void UpdateUserPassword(string hashedPassword)
        {
            HashedPassword = hashedPassword;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public void UpdateLastLoginDate()
        {
            LastLoginDate = DateTime.UtcNow;
        }
    }
}
