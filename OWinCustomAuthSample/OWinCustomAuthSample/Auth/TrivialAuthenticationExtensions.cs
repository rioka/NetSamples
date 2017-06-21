using Owin;

namespace OWinCustomAuthSample.Auth
{
  public static class TrivialAuthenticationExtensions
  {
    #region Apis

    public static void UseTrivialAuthentication(this IAppBuilder appBuilder)
    {
      appBuilder.Use<TrivialAuthenticationMiddleware>(new TrivialAuthenticationOptions());
    }

    #endregion
  }
}