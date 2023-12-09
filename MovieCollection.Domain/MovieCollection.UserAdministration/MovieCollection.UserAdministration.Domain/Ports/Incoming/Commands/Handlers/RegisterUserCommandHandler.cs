using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Text.RegularExpressions;
using MovieCollection.UserAdministration.Domain.Entities;
using MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Results;
using MovieCollection.UserAdministration.Domain.Ports.OutGoing;
using MovieCollection.UserAdministration.Domain.Utility;
using MovieCollection.UserAdministration.Domain.DTOs;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Handlers
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserResult>
    {
        private IUserAdministrationPersistence _userAdministrationPersistence;

        public RegisterUserCommandHandler(IUserAdministrationPersistence userAdministrationPersistence)
        {
            _userAdministrationPersistence = userAdministrationPersistence;
        }

        public async Task<RegisterUserResult> Handle(RegisterUserCommand command)
        {
            var registerUserResult = new RegisterUserResult();
            var user = await _userAdministrationPersistence.GetUserAsync(command.Email);
            if (user != null)
                return registerUserResult.UserAlreadyExistError();

            if (!IsValidEmail(command.Email))
                return registerUserResult.InvaildEmailError();

            if (!PasswordValidator.Validate(command.Password))
                return registerUserResult.InvaildPasswordError();

            var hasher = new PasswordHasher<string>();
            var hashedPassword = hasher.HashPassword(command.Email, command.Password);

            var newUser = new User(command.Email, hashedPassword, command.FirstName, command.LastName, command.UserRole);
            await _userAdministrationPersistence.AddUpdateUserAsync(newUser);

            return registerUserResult.Success(new UserEntityDto(newUser.Id, newUser.Email, newUser.FirstName, newUser.LastName));
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress emailAddress = new(email);
            }
            catch (Exception)
            {
                return false;
            }

            // MailAddress does not do domain name validation. Do partial validation to check expected domain format
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                return false;
            }

            return true;
        }
    }
}
