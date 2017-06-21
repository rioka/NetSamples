using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace OWinCustomAuthSample
{
  public class WebApiConfig
  {
    #region Apis

    internal static void Register(HttpConfiguration config)
    {
      config.MapHttpAttributeRoutes();

      var settings = config.Formatters.JsonFormatter.SerializerSettings;
      settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

      config.Filters.Add(new AuthorizeAttribute());
    }

    #endregion
  }
}