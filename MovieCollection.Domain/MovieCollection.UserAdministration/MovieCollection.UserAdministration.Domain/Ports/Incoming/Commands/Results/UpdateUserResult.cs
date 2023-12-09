namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Results
{
    public class UpdateUserResult
    {
        public bool UserNotFound { get; private set; }
        public bool EmailAlreadyExist { get; private set; }
        public bool IsSuccess { get; private set; }

        public UpdateUserResult UserNotFoundError() => new UpdateUserResult { UserNotFound = true };
        public UpdateUserResult EmailAlreadyExistError() => new UpdateUserResult { EmailAlreadyExist = true };
        public UpdateUserResult Success() => new UpdateUserResult { IsSuccess = true };
    }
}
