using Microsoft.AspNetCore.Identity;
using MovieCollection.UserAdministration.Domain.Entities;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Events;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Infrastructure;
using MovieCollection.UserAdministration.Domain.Ports.OutGoing;
using MovieCollection.UserAdministration.Domain.Utility;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Handlers
{
    public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, UserToken>
    {
        private readonly TokenFactory _tokenFactory;
        private IUserAdministrationPersistence _userAdministrationPersistence;
        private IEventDispatcher _eventDispatcher;

        public AuthenticateCommandHandler(TokenFactory tokenFactory, IUserAdministrationPersistence userAdministrationPersistence, IEventDispatcher eventDispatcher)
        {
            _tokenFactory = tokenFactory;
            _userAdministrationPersistence = userAdministrationPersistence;
            _eventDispatcher = eventDispatcher;
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
            
            if (passwordVerificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                await _eventDispatcher.RaiseEventAsync(new ReHashedPasswordEvent(user, command.Password));
                return _tokenFactory.CreateUserToken(user.Id, command.Email, user.UserRole);
            }

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            throw new Exception("Inavlid password");

            return _tokenFactory.CreateUserToken(user.Id, command.Email, user.UserRole);
        }
    }
}
