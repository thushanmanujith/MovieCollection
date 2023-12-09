using MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Results;
using MovieCollection.UserAdministration.Domain.Ports.OutGoing;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Handlers
{
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UpdateUserResult>
    {
        private IUserAdministrationPersistence _userAdministrationPersistence;
        public UpdateUserCommandHandler(IUserAdministrationPersistence userAdministrationPersistence)
        {
            _userAdministrationPersistence = userAdministrationPersistence;
        }

        public async Task<UpdateUserResult> Handle(UpdateUserCommand command)
        {
            var updateUserResult = new UpdateUserResult();

            var user = await _userAdministrationPersistence.GetUserAsync(command.Id);
            if (user == null)
                return updateUserResult.UserNotFoundError();

            if (user.Email != command.Email)
            {
                var exsitingUser = await _userAdministrationPersistence.GetUserAsync(command.Email);
                if (exsitingUser != null)
                    return updateUserResult.EmailAlreadyExistError();
            }
            user.UpdateUser(command.Email, command.FirstName, command.LastName);

            await _userAdministrationPersistence.AddUpdateUserAsync(user);

            return updateUserResult.Success();
        }
    }
}
