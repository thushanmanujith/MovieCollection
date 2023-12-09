using MovieCollection.UserAdministration.Domain.Entities;
using MovieCollection.UserAdministration.Domain.Ports.OutGoing;
using Microsoft.EntityFrameworkCore;

namespace MovieCollection.UserAdministration.Persistence
{
    public class UserAdministrationPersistence : IUserAdministrationPersistence
    {
        protected readonly UserAdministrationDataContext _dataContext;

        public UserAdministrationPersistence(UserAdministrationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _dataContext.User.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _dataContext.User.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<User> AddUpdateUserAsync(User user)
        {
            var entity = _dataContext.Update(user);
            await _dataContext.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<IList<User>> GetUsersAsync()
        {
            return await _dataContext.User.ToListAsync();
        }
    }
}
