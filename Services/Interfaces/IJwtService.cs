using System.Security.Claims;

namespace AuthenticationDemo.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(string email, string role);
        object GenerateJwtToken(string email);

        ClaimsPrincipal? ValidateToken(string token);
    }
}
