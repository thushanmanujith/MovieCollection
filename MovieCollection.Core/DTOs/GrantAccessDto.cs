using MovieCollection.Core.Enums;

namespace MovieCollection.Core.DTOs
{
    public class GrantAccessDto
    {
        public int UserId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
