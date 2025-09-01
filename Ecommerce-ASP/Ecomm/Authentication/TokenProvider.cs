using System.Security.Claims;
using System.Text;
using Ecomm.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace CalConnect.Api.Users.Infrastructure;

public class TokenProvider(IConfiguration configuration)
{
    public string Create(User user)
    {
        var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim(ClaimTypes.Role, user.role.ToString()),
                new Claim("username", user.username),
                new Claim("email_verified", user.isActive.ToString())
            ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        var token = handler.CreateToken(tokenDescriptor);

        return token;
    }

    public string? GetUsernameByJwt(HttpContext context)
    {
        var identity = context.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            var claims = identity.Claims;
            var username = claims.FirstOrDefault(c => c.Type == "username")?.Value;
            return username;
        }

        return null;
    }

    public Guid? GetIdByJwt(HttpContext context)
    {
        var identity = context.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            var claims = identity.Claims;
            var id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var identification = new Guid(id);
            return identification;
        }

        return null;
    }
}