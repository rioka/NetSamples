using Microsoft.Owin.Security;

namespace OWinCustomAuthSample.Auth
{
  public class TrivialAuthenticationOptions : AuthenticationOptions
  {
    #region Constructors

    public TrivialAuthenticationOptions() : base("x-trivial-auth") { }

    #endregion
  }
}