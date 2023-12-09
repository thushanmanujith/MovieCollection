using MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Results;
using MovieCollection.UserAdministration.Domain.Ports.OutGoing;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Handlers
{
    public class GrantAccessCommandHandler : ICommandHandler<GrantAccessCommand, GrantAccessResult>
    {
        private IUserAdministrationPersistence _userAdministrationPersistence;
        public GrantAccessCommandHandler(IUserAdministrationPersistence userAdministrationPersistence)
        {
            _userAdministrationPersistence = userAdministrationPersistence;
        }

        public async Task<GrantAccessResult> Handle(GrantAccessCommand command)
        {
            var grantAccessResult = new GrantAccessResult();

            var user = await _userAdministrationPersistence.GetUserAsync(command.UserId);

            if (user == null) return grantAccessResult.UserNotFoundError();

            user.SetUserRole(command.UserRole);

            await _userAdministrationPersistence.AddUpdateUserAsync(user);

            return grantAccessResult.Success();
        }
    }
}
