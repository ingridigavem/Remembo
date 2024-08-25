using Microsoft.IdentityModel.Tokens;
using Remembo.Domain;
using Remembo.Domain.Account.Entities;
using Remembo.Domain.Account.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Remembo.Service.Account;
public class TokenService : ITokenService {

    public string GenerateToken(User user) {
        var jwtKey = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);

        var tokenHandler = new JwtSecurityTokenHandler();

        var credentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(GetUserClaims(user)),
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static IEnumerable<Claim> GetUserClaims(User user) {
        return new List<Claim> {
            new (ClaimTypes.Email, user.Email),
            new ("name", user.Name),
            new ("userId", user.Id.ToString())
        };
    }
}
