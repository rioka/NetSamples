using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;

namespace OWinCustomAuthSample.Auth
{
  public class TrivialAuthenticationMiddleware : AuthenticationMiddleware<TrivialAuthenticationOptions>
  {
    #region Constructors

    public TrivialAuthenticationMiddleware(OwinMiddleware nextMiddleware, TrivialAuthenticationOptions options)
       : base(nextMiddleware, options) { }

    #endregion

    #region Overrides

    protected override AuthenticationHandler<TrivialAuthenticationOptions> CreateHandler()
    {
      return new TrivialAuthenticationHandler();
    }


    #endregion
  }
}