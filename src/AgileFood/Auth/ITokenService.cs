using AgileFood.Application.Dtos.Users;

namespace AgileFood.Api.Auth;

public record TokenResult(string Token, DateTime ExpiresAtUtc);

public interface ITokenService
{
    TokenResult GenerateToken(UserResultDto user);
}
