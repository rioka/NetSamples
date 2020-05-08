using System.Data;
using System.Data.SqlClient;
using Autofac;
using Autofac.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacSamples.Scenarios
{
  [TestClass]
  public class CanRegisterSameTypeWithDifferentParameters
  {
    #region Data

    private ContainerBuilder _builder;
    private IContainer _container;

    #endregion

    #region Setup & teardown

    [TestInitialize]
    public void BeforeEach()
    {
      _builder = new ContainerBuilder();

      _builder.RegisterType<SqlConnection>()
        .WithParameter(new TypedParameter(typeof(string), @"Data Source=(localdb)\v11.0;Initial Catalog=A"))
        .AsSelf()
        .Keyed<IDbConnection>("v11.0");

      _builder.RegisterType<SqlConnection>()
        .WithParameter(new TypedParameter(typeof(string), @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=B"))
        .AsSelf()
        .Keyed<IDbConnection>("MSSqlLocalDb");

      _builder.RegisterType<OldRepository>()
        .AsImplementedInterfaces()
        .WithParameter(new ResolvedParameter((p, c) => p.ParameterType == typeof(IDbConnection),
                                             (p, c) => c.ResolveKeyed<IDbConnection>("v11.0")));

      _builder.RegisterType<NewRepository>()
        .AsImplementedInterfaces()
        .WithParameter(new ResolvedParameter((p, c) => p.ParameterType == typeof(IDbConnection),
                                             (p, c) => c.ResolveKeyed<IDbConnection>("MSSqlLocalDb")));

      _builder.RegisterType<FakeDependency1>()
        .AsSelf();
      _builder.RegisterType<FakeDependency2>()
        .AsSelf();

      _container = _builder.Build();
    }

    [TestCleanup]
    public void AfterEach()
    {
      _container?.Dispose();
    }

    #endregion

    #region Tests

    [TestMethod]
    public void Can_Resolve_Same_Type_With_Different_Parameters()
    {
      // arrange
      
      // act
      var oldRepo = _container.Resolve<IOldRepository>();
      var newRepo = _container.Resolve<INewRepository>();

      // assert
      Assert.IsTrue(oldRepo.ConnectionString.Contains("v11.0"));
      Assert.IsTrue(newRepo.NewConnectionString.Contains("MSSqlLocalDb"));
    }

    #endregion

    #region Types

    interface IOldRepository
    {
      string ConnectionString { get; }
    }

    class OldRepository : IOldRepository
    {
      private readonly IDbConnection _cn;
      private readonly FakeDependency2 _fd2;

      public string ConnectionString => _cn.ConnectionString;

      public OldRepository(IDbConnection cn, FakeDependency2 fd2)
      {
        _cn = cn;
        _fd2 = fd2;
      }
    }

    interface INewRepository
    {
      string NewConnectionString { get; }
    }

    class NewRepository : INewRepository
    {
      private readonly IDbConnection _cn;
      private readonly FakeDependency1 _fd1;
      private readonly FakeDependency2 _fd2;

      public string NewConnectionString => _cn.ConnectionString;

      public NewRepository(IDbConnection cn, FakeDependency1 fd1, FakeDependency2 fd2)
      {
        _cn = cn;
        _fd1 = fd1;
        _fd2 = fd2;
      }
    }

    class FakeDependency1 { }

    class FakeDependency2 { }

    #endregion
  }
}
