using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwitterAnalysis.App.Service.Model.Settings;
using TwitterAnalysis.Application.Auth.Interfaces;
using TwitterAnalysis.Application.Messages.Request.Auth;

namespace TwitterAnalysis.Infrastructure.Auth.Jwt
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }

        public string GenerateToken(LoginAuthRequest login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var keyDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login.Name)
                }),

                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(keyDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
