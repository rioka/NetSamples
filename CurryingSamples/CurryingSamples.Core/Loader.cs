using System;
using CurryingSamples.Core.Models;

namespace CurryingSamples.Core
{
  public class Loader
  {
    public Customer Load(Func<string, Customer> factory)
    {
      // get the level somehow
      var level = "Level" + DateTime.Now.Millisecond.ToString();
      return factory(level);
    }
  }
}
