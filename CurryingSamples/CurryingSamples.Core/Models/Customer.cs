
namespace CurryingSamples.Core.Models
{
  public class Customer
  {
    public string Name { get; private set; }

    public string Level { get; private set; }

    public Customer(string name)
    {
      Name = name;
    }

    public void SetLevel(string level)
    {
      Level = level;
    }

    public static Customer FromName(string name)
    {
      return new Customer(name);
    }
  }
}
