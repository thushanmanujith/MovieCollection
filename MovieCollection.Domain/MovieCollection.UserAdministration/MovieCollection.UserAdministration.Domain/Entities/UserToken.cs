namespace MovieCollection.UserAdministration.Domain.Entities
{
    public class UserToken
    {
        public UserToken(string accessToken, string refreshToken, int expiresIn, string accessClaim)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            AccessClaims = new[] { accessClaim };
            ExpiresIn = expiresIn;
        }

        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string[] AccessClaims { get; set; }
        public string RefreshToken { get; set; }
    }
}
