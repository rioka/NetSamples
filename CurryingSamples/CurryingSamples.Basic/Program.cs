using System;
using CurryingSamples.Basic.Models;

namespace CurryingSamples.Basic
{
  class Program
  {
    /// <summary>
    /// We define a function returning a function: the first function has "name"
    /// as parameter, and return a second function which expect "level" as parameter
    /// This second function create a customer from the given name and then
    /// set the level for the customer.
    /// The customer is the returned
    /// So, at the end
    /// - Restorer("John") returns a function which has 1 parameter ("level")
    /// - When this second function is called, we get a customer with values
    ///   from the two parameters
    /// </summary>
    /// <remarks>
    /// The full code wuold be 
    /// <code>
    /// Func<string, Func<string, Customer>> Restorer = name => 
    ///   new Func<string, Customer>(level => {...});
    /// </code>
    /// Anyway, the returned function’s type declaration (ie Func&lt;string, Customer&gt;)
    /// can be inferred by the compiler, so we can use the short version below
    /// </remarks>
    private static Func<string, Func<string, Customer>> Restorer = name => level => {
      var customer = Customer.FromName(name);
      customer.SetLevel(level);
      return customer;
    };

    static void Main(string[] args)
    {
      var name = "John";
      var loader = new Loader();
      var customer = loader.Load(Restorer(name));

      Console.WriteLine("Name\t{0}\nLevel\t{1}",customer.Name, customer.Level);
      Console.ReadLine();
    }
  }
}
