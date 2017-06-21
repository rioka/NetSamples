using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace OWinCustomAuthSample.Auth
{
  public class TrivialAuthenticationHandler : AuthenticationHandler<TrivialAuthenticationOptions>
  {
    #region Overrides
    
    protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
    {
      var authorized = CheckAuthorization(Request.Headers);

      if (authorized)
      {
        var now = DateTime.UtcNow;
        var properties = new AuthenticationProperties {
          AllowRefresh = true,
          ExpiresUtc = now.AddDays(7),
          IssuedUtc = now,
          IsPersistent = true
        };
        var claimCollection = new List<Claim> {
          new Claim(ClaimTypes.Name, ""),
          new Claim(ClaimTypes.Email, "me@example.com"),
          new Claim(ClaimTypes.Role, "WTF")
        };
        var claimsIdentity = new ClaimsIdentity(claimCollection, "Custom");
        var ticket = new AuthenticationTicket(claimsIdentity, properties);

        return Task.FromResult(ticket);
      }

      return Task.FromResult(default(AuthenticationTicket));
    }

    #endregion

    bool CheckAuthorization(IHeaderDictionary headers)
    {
      string[] hdrs;

      // real stuff here
      if (headers.TryGetValue("x-trivial-auth", out hdrs)
          && !string.IsNullOrWhiteSpace(hdrs.FirstOrDefault()))
      {
        return true;
      }

      return false;
    }
  }
}