
using System;
using CurryingSamples.Core;
using CurryingSamples.Core.Models;
using CurryingSamples.Generalization.Extensions;

namespace CurryingSamples.Generalization
{
  class Program
  {
    /// <summary>
    /// Our custom function to restore, written the "classic" way
    /// We then apply currying via an extension method, and
    /// pass the resulting function to <see cref="Loader.Load"/>
    /// </summary>
    private static Func<string, string, Customer> Restorer = (name, level) => {
      var customer = Customer.FromName(name);
      customer.SetLevel(level);
      return customer;
    };

    static void Main(string[] args)
    {
      var name = "John";
      var loader = new Loader();
      var customer = loader.Load(Restorer.Curry()(name));

      Console.WriteLine("Name\t{0}\nLevel\t{1}\nPress ENTER to quit", customer.Name, customer.Level);
      Console.ReadLine();
    }
  }
}
