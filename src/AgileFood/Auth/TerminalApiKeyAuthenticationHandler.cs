using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace AgileFood.Api.Auth;

public class TerminalApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IConfiguration _configuration;

    public TerminalApiKeyAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IConfiguration configuration)
        : base(options, logger, encoder)
    {
        _configuration = configuration;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(TerminalApiKeyDefaults.HeaderName, out var providedKey) ||
            string.IsNullOrWhiteSpace(providedKey))
        {
            return Task.FromResult(AuthenticateResult.Fail("Chave do terminal não informada."));
        }

        var expectedKey = _configuration["Terminal:ApiKey"];

        if (!IsValidKey(providedKey.ToString(), expectedKey))
            return Task.FromResult(AuthenticateResult.Fail("Chave do terminal inválida."));

        var identity = new ClaimsIdentity(
            [new Claim(ClaimTypes.NameIdentifier, "terminal")],
            TerminalApiKeyDefaults.AuthenticationScheme);

        var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), TerminalApiKeyDefaults.AuthenticationScheme);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    private static bool IsValidKey(string providedKey, string? expectedKey)
    {
        if (string.IsNullOrWhiteSpace(expectedKey))
            return false;

        var providedBytes = Encoding.UTF8.GetBytes(providedKey);
        var expectedBytes = Encoding.UTF8.GetBytes(expectedKey);

        return providedBytes.Length == expectedBytes.Length &&
               CryptographicOperations.FixedTimeEquals(providedBytes, expectedBytes);
    }
}
