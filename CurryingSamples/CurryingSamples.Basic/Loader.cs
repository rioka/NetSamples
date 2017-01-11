using System;
using CurryingSamples.Basic.Models;

namespace CurryingSamples.Basic
{
  internal class Loader
  {
    public Customer Load(Func<string, Customer> factory)
    {
      // get the level somehow
      var level = "Level" + DateTime.Now.Millisecond.ToString();
      return factory(level);
    }
  }
}
