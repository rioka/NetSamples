using Owin;
using OWinCustomAuthSample.Auth;

namespace OWinCustomAuthSample
{
  public partial class Startup
  {
    internal static void ConfigureAuth(IAppBuilder appBuilder)
    {
      appBuilder.UseTrivialAuthentication();
    }
  }
}