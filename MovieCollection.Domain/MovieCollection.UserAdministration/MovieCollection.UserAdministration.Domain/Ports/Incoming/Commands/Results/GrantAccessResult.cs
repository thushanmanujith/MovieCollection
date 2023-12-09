namespace MovieCollection.UserAdministration.Domain.Ports.Incoming.Commands.Results
{
    public class GrantAccessResult
    {
        public bool UserNotFound { get; private set; }
        public bool IsSuccess { get; private set; }

        public GrantAccessResult UserNotFoundError() => new GrantAccessResult { UserNotFound = true };
        public GrantAccessResult Success() => new GrantAccessResult { IsSuccess = true };
    }
}
