using System;
using System.Diagnostics;
using Ninject;

namespace MultipleConstructors.Core.WithInjectAttribute
{
  public interface IUploader3
  {
    bool ParameterlessCtorUsed { get; }
  }

  public class Uploader3 : IUploader3
  {
    #region Vars
    
    readonly IConnector3 _connector;
    
    public bool ParameterlessCtorUsed { get; private set; }

    #endregion

    #region Constructors

    public Uploader3() : this(new Connector3())
    {
      ParameterlessCtorUsed = true;
      Console.WriteLine("Something went wrong: should not use this!");
      Debug.WriteLine("Something went wrong: should not use this!");
    }

    [Inject]
    public Uploader3(Connector3 connector)
    {
      _connector = connector;
    }

    #endregion
  }
}
