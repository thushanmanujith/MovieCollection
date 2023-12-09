using MovieCollection.UserAdministration.Domain.Entities;
using MovieCollection.UserAdministration.Domain.Enums;

namespace MovieCollection.UserAdministration.Domain.Ports.OutGoing
{
    public interface IUserAdministrationPersistence
    {
        Task<User> GetUserAsync(string email);
        Task<User> GetUserAsync(int id);
        Task<User> AddUpdateUserAsync(User user);
        Task<IList<User>> GetUsersAsync();
    }
}
