using System;
using Formo;

namespace FormoSamples.AppSettingsSample
{
  class Program
  {
    static void Main(string[] args)
    {
      dynamic config = new Configuration();

      // grab the values 
      GrabAsValues(config);
      // grab the value into a custom class
      GrabAsCustomType(config);

      // did not investigate configSecions as it only support namevaluecollection, so just appSettings with another name
    }

    #region Internals

    private static void GrabAsValues(dynamic config)
    {
      // grab the value

      Console.WriteLine("{0}\n\t{1}", "AxServiceUrl", config.AxServiceUrl);
      Console.WriteLine("{0}\n\t{1}", "AxServiceUsername", config.AxServiceUsername);
      Console.WriteLine("{0}\n\t{1}", "AxServicePassword", config.AxServicePassword);

      // provide a default value if not set
      Console.WriteLine("{0}\n\t{1}", "AxServiceDomain", config.AxServiceDomain("DefaultDomain"));

      // cast to a specific type
      var timeout = config.AxServiceTimeout<long>();
      Console.WriteLine("{0}\n\t{1} ({2})", "AxServiceTimeout", timeout, timeout.GetType());
    }

    private static void GrabAsCustomType(dynamic config)
    {
      var settings = config.Bind<AxServiceSettings>();

      Console.WriteLine("{0}\n\t{1}", "AxServiceUrl", settings.AxServiceUrl);
      Console.WriteLine("{0}\n\t{1}", "AxServiceUsername", settings.AxServiceUsername);
      Console.WriteLine("{0}\n\t{1}", "AxServicePassword", settings.AxServicePassword);

      // cannot provide a default value in this case
      Console.WriteLine("{0}\n\t{1}", "AxServiceDomain", settings.AxServiceDomain);

      // cannot cast to a specific type in this case
      var timeout = Convert.ChangeType(settings.AxServiceTimeout, TypeCode.Int64);
      Console.WriteLine("{0}\n\t{1} ({2})", "AxServiceTimeout", timeout, timeout.GetType());
    }

    #endregion
  }
}
