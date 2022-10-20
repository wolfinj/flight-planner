using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace FlightPlanner.Helpers;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IEntityService<User> _userService;
    
    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,IEntityService<User> entityService) : base(options, logger, encoder, clock)
    {
        _userService = entityService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // skip authentication if endpoint has [AllowAnonymous] attribute
        var endpoint = Context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            return AuthenticateResult.NoResult();

        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Missing Authorization Header");

        var authorized = false;

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var username = credentials[0];
            var paswordBytes = Encoding.UTF8.GetBytes(credentials[1].ToCharArray());
            var password =Convert.ToBase64String(paswordBytes) ;
            
            authorized = _userService.Query().Any(u => u.UserName == username && u.Password == password);
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }

        if (authorized == false)
            return AuthenticateResult.Fail("Invalid Username or Password");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "user"),
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
