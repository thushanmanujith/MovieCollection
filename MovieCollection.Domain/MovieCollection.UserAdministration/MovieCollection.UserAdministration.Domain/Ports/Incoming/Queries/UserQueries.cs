using MovieCollection.UserAdministration.Domain.DTOs;
using MovieCollection.UserAdministration.Domain.Ports.OutGoing;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Queries
{
    public class UserQueries : IUserQueries
    {
        private IUserAdministrationPersistence _userAdministrationPersistence;

        public UserQueries(IUserAdministrationPersistence userAdministrationPersistence)
        {
            _userAdministrationPersistence = userAdministrationPersistence;
        }

        public async Task<UserEntityDto> GetUserAsync(string email)
        {
            var user = await _userAdministrationPersistence.GetUserAsync(email);
            return new UserEntityDto(user.Id, user.Email, user.FirstName, user.LastName);
        }

        public async Task<UserEntityDto> GetUserAsync(int id)
        {
            var user = await _userAdministrationPersistence.GetUserAsync(id);
            return new UserEntityDto(user.Id, user.Email, user.FirstName, user.LastName);
        }

        public async Task<IEnumerable<UserEntityDto>> GetUsersAsync()
        {
            var users = await _userAdministrationPersistence.GetUsersAsync();
            return users.Select(user => new UserEntityDto(user.Id, user.Email, user.FirstName, user.LastName));
        }
    }
}
