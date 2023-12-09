using MovieCollection.UserAdministration.Domain.DTOs;

namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Results
{
    public class RegisterUserResult
    {
        public bool InvaildPassword { get; private set; }
        public bool InvaildEmail { get; private set; }
        public bool UserAlreadyExist { get; private set; }
        public bool IsSuccess { get; private set; }
        public UserEntityDto User { get; private set; }

        public RegisterUserResult InvaildPasswordError() => new RegisterUserResult { InvaildPassword = true };
        public RegisterUserResult InvaildEmailError() => new RegisterUserResult { InvaildEmail = true };
        public RegisterUserResult UserAlreadyExistError() => new RegisterUserResult { UserAlreadyExist = true };
        public RegisterUserResult Success(UserEntityDto user) => new RegisterUserResult { IsSuccess = true, User = user };
    }
}
