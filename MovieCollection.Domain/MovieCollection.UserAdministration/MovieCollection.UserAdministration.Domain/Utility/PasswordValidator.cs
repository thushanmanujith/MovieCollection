namespace MovieCollection.UserAdministration.Domain.Utility
{
    public static class PasswordValidator
    {
        public static bool Validate(string password)
        {
            var hasLength = password.Length >= 8;
            var hasDigit = password.Any(char.IsDigit);

            return hasLength && hasDigit;
        }
    }
}
