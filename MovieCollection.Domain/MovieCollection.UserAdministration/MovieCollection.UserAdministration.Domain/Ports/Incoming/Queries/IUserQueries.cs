using MovieCollection.UserAdministration.Domain.DTOs;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Queries
{
    public interface IUserQueries
    {
        Task<UserEntityDto> GetUserAsync(string email);
        Task<UserEntityDto> GetUserAsync(int id);
        Task<IEnumerable<UserEntityDto>> GetUsersAsync();
    }
}
