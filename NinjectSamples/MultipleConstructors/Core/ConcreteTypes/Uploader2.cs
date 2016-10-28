using System;
using System.Diagnostics;

namespace MultipleConstructors.Core.ConcreteTypes
{
  public interface IUploader2
  {
    bool ParameterlessCtorUsed { get; }
  }

  public class Uploader2 : IUploader2
  {
    #region Vars
    
    readonly IConnector2 _connector;
    
    public bool ParameterlessCtorUsed { get; private set; }

    #endregion

    #region Constructors

    public Uploader2() : this(new Connector2())
    {
      ParameterlessCtorUsed = true;
      Console.WriteLine("This is OK: should use the parameterless ctor!");
      Debug.WriteLine("This is OK: should use the parameterless ctor!");
    }

    public Uploader2(Connector2 connector)
    {
      _connector = connector;
    }

    #endregion
  }
}
