using Microsoft.AspNetCore.Identity;
using MovieCollection.UserAdministration.Domain.Entities;
using MovieCollection.UserAdministration.Domain.Ports.OutGoing;
using MovieCollection.UserAdministration.Domain.Utility;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Handlers
{
    public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, UserToken>
    {
        private readonly TokenFactory _tokenFactory;
        private IUserAdministrationPersistence _userAdministrationPersistence;
        public AuthenticateCommandHandler(TokenFactory tokenFactory, IUserAdministrationPersistence userAdministrationPersistence)
        {
            _tokenFactory = tokenFactory;
            _userAdministrationPersistence = userAdministrationPersistence;
        }

        public async Task<UserToken> Handle(AuthenticateCommand command)
        {
            if (string.IsNullOrEmpty(command.Email) || string.IsNullOrEmpty(command.Password))
                throw new Exception("Invalid credentials");

            var user = await _userAdministrationPersistence.GetUserAsync(command.Email);
            if (user == null)
                throw new Exception("User not found");

            var hasher = new PasswordHasher<string>();
            var passwordVerificationResult = hasher.VerifyHashedPassword(command.Email, user.HashedPassword, command.Password);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
                throw new Exception("Inavlid password");

            return _tokenFactory.CreateUserToken(user.Id, command.Email, user.UserRole);
        }
    }
}
