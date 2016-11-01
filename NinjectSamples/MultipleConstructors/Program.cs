
using System;
using System.Diagnostics;
using MultipleConstructors.Core.ConcreteTypes;
using MultipleConstructors.Core.InterfacesOnly;
using MultipleConstructors.Core.WithInjectAttribute;
using Ninject;

namespace MultipleConstructors
{
  class Program
  {
    static void Main(string[] args)
    {
      UseContructorWithParameters();
      UseConstructorWithoutParameters();
      UseConstructorWitParametersIfConcreteTypesAreKnown();
      UseConstructorWithParametersMarkedWithAttribute();
    }

    static void UseContructorWithParameters()
    {
      Console.WriteLine("Instantiating {0}...", typeof(Uploader));

      var kernel = new StandardKernel();

      // Dependancies are injected as interfaces
      kernel.Bind<IConnector>().To<Connector>();
      kernel.Bind<IUploader>().To<Uploader>();

      var uploader = kernel.Get<IUploader>();

      if (!uploader.ParameterlessCtorUsed)
      {
        Console.WriteLine("\tThis is OK: did not use the parameterless ctor!");
        Debug.WriteLine("\tThis is OK: did not use the parameterless ctor!");
      }

      kernel.Dispose();
    }

    static void UseConstructorWithoutParameters()
    {
      Console.WriteLine("Instantiating {0}...", typeof(Uploader2));

      var kernel = new StandardKernel();

      // Dependancies are injected as concrete types
      // do not register IConnector: Ninject will be able to resolve it anyway 
      // but Uploader2 will use the constructor without parameters
      kernel.Bind<IUploader2>().To<Uploader2>();

      var uploader = kernel.Get<IUploader2>();
      if (!uploader.ParameterlessCtorUsed)
      {
        Console.WriteLine("\tSomething went wrong: should have passed through the parameterless constructor!");
        Debug.WriteLine("\tSomething went wrong: should have passed through the parameterless constructor!");
      }

      kernel.Dispose();
    }

    static void UseConstructorWitParametersIfConcreteTypesAreKnown()
    {
      Console.WriteLine("Instantiating {0} registering all concrete types...", typeof(Uploader2));

      var kernel = new StandardKernel();

      // Dependancies are injected as concrete types
      // Do register Connector: Ninject will be able to resolve it immediately
      // so will use the parameterless ctor 
      kernel.Bind<Connector2>().ToSelf();
      kernel.Bind<IUploader2>().To<Uploader2>();

      var uploader = kernel.Get<IUploader2>();
      if (uploader.ParameterlessCtorUsed)
      {
        Console.WriteLine("\tSomething went wrong: should not have passed through the parameterless constructor!");
        Debug.WriteLine("\tSomething went wrong: should not have passed through the parameterless constructor!");
      }
      else
      {
        Console.WriteLine("\tThis is OK: the parameterless constructor has not been used!");
        Debug.WriteLine("\tThis is OK: the parameterless constructor has not been used!");
      }

      kernel.Dispose();
    }

    static void UseConstructorWithParametersMarkedWithAttribute() 
    {
      Console.WriteLine("Instantiating {0}...", typeof(Uploader3));

      var kernel = new StandardKernel();

      // Dependancies are injected as concrete types
      // do not register IConnector3: Ninject will be able to resolve it anyway 
      // and Uploader3 will use the constructor marked with Inject
      kernel.Bind<IUploader3>().To<Uploader3>();

      var uploader = kernel.Get<IUploader3>();
      if (uploader.ParameterlessCtorUsed) 
      {
        Console.WriteLine("\tSomething went wrong: should not have passed through the parameterless constructor!");
        Debug.WriteLine("\tSomething went wrong: should not have passed through the parameterless constructor!");
      }
      else 
      {
        Console.WriteLine("\tThis is OK: the parameterless constructor has not been used!");
        Debug.WriteLine("\tThis is OK: the parameterless constructor has not been used!");
      }

      kernel.Dispose();
    }
  }
}
