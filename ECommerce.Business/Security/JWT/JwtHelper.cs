using ECommerce.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Business.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;

        public JwtHelper(IConfiguration configuration)
        {
            _tokenOptions = configuration
                .GetSection("TokenOptions")
                .Get<TokenOptions>()!;
        }

        public AccessToken CreateToken(User user)
        {
            var expiration = DateTime.Now.AddMinutes(
                _tokenOptions.AccessTokenExpiration);

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: expiration,
                claims: claims,
                signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = expiration,
                Role = user.Role
            };
        }
    }
}