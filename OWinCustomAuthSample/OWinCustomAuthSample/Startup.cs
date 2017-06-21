using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(OWinCustomAuthSample.Startup))]
namespace OWinCustomAuthSample
{
  public partial class Startup
  {
    #region Apis

    public void Configuration(IAppBuilder app)
    {
      var config = new HttpConfiguration();
      WebApiConfig.Register(config);

      ConfigureAuth(app);

      app.UseWebApi(config);
    }

    #endregion
  }
}