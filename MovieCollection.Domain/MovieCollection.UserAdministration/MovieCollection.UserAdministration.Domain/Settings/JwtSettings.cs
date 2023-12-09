namespace MovieCollection.UserAdministration.Domain.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
    }
}
