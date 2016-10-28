
using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace MultipleConstructors.Core.InterfacesOnly
{
  public interface IUploader
  {
    bool ParameterlessCtorUsed { get; }
  }

  public class Uploader : IUploader
  {
    #region Vars
    
    readonly IConnector _connector;

    public bool ParameterlessCtorUsed { get; private set; }

    #endregion

    #region Constructors

    public Uploader() : this(new Connector())
    {
      ParameterlessCtorUsed = true;
      Console.WriteLine("Something went wrong: should not use this!");
      Debug.WriteLine("Something went wrong: should not use this!");
    }

    public Uploader(IConnector connector)
    {
      _connector = connector;
    }

    #endregion
  }
}
