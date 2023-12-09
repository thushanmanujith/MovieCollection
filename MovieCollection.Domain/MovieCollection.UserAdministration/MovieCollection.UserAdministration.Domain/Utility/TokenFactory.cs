using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MovieCollection.UserAdministration.Domain.Constant;
using MovieCollection.UserAdministration.Domain.Enums;
using MovieCollection.UserAdministration.Domain.Entities;
using MovieCollection.UserAdministration.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace MovieCollection.UserAdministration.Domain.Utility
{
    public class TokenFactory
    {
        private readonly JwtSettings _jwtSettings;

        public TokenFactory(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public UserToken CreateUserToken(int UserId, string Email, UserRole userRole)
        {
            var claims = new List<Claim>
            {
                new Claim(UserClaims.UserId, UserId.ToString(), ClaimValueTypes.Integer32),
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Role, userRole.ToString())
            };

            return new UserToken(GenerateAccessToken(claims), GenerateRefreshToken(), AccountSettings.AccessTokenExpiryInMinutes * 60, userRole.ToString());
        }

        private string GenerateAccessToken(List<Claim> claims)
        {
            var secuirtykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(secuirtykey, SecurityAlgorithms.HmacSha256Signature);

            var expiresAt = DateTime.UtcNow.AddMinutes(AccountSettings.AccessTokenExpiryInMinutes);

            var tokenDescriptor = new JwtSecurityToken(_jwtSettings.Issuer, null, claims,
            expires: expiresAt,
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
