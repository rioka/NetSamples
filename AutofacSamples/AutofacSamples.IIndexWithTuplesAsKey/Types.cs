using System;
using System.Reflection;
using Autofac.Features.Indexed;

namespace AutofacSamples.IIndexWithTuplesAsKey
{
  #region IRepository

  /// <summary>
  /// Interface
  /// </summary>
  public interface IRepository { }

  #endregion

  #region Custom attribute 

  /// <summary>
  /// Attribute to link a type to its registration properties
  /// </summary>
  public class VersionAttribute : Attribute
  {
    public int Version { get; set; }

    public string Content { get; set; }

    public VersionAttribute(int version) : this(version, string.Empty)
    { }

    public VersionAttribute(int version, string content)
    {
      Version = version;
      Content = content;
    }
  }

  #endregion

  #region IRepository implementations

  /// <summary>
  /// First implementation for <see cref="IRepository"/>, to be registered for version 1 and tag "A"
  /// </summary>
  [Version(1, "A")]
  public class RepositoryA : IRepository { }

  /// <summary>
  /// Another implementation for <see cref="IRepository"/>, to be registered for version 1, no tag
  /// </summary>
  [Version(1)]
  public class RepositoryB : IRepository { }

  /// <summary>
  /// Another implementation for <see cref="IRepository"/>, to be registered for version 2 and tag "A"
  /// </summary>
  [Version(2)]
  public class RepositoryC : IRepository { }

  #endregion

  #region Factory

  /// <summary>
  /// Factory to return a <see cref="IRepository"/> according to a version and a tag
  /// </summary>
  public class RepositoryFactory
  {
    #region Data

    private readonly IIndex<Tuple<int, string>, IRepository> _map;

    #endregion

    #region Constructors

    public RepositoryFactory(IIndex<Tuple<int, string>, IRepository> map)
    {
      _map = map;
    }

    #endregion

    #region APIs

    /// <summary>
    /// Get a <see cref="IRepository"/> registered for the given version and tag
    /// </summary>
    /// <param name="version">A version</param>
    /// <param name="content">A tag</param>
    /// <returns></returns>
    public IRepository Get(int version, string content)
    {
      IRepository instance;

      if (_map.TryGetValue(Tuple.Create(version, content ?? string.Empty), out instance))
      {
        return instance;
      }

      throw new Exception("No matching type");
    }

    #endregion
  }

  #endregion

  #region Component

  /// <summary>
  /// Sample service depending on <see cref="IRepository"/>
  /// </summary>
  public class Service
  {
    #region Data

    private readonly RepositoryFactory _factory;

    #endregion

    #region Constructor

    public Service(RepositoryFactory factory)
    {
      _factory = factory;
    }

    #endregion

    #region APIs

    public void Do(int value, string content = null)
    {

      Console.WriteLine($"  Input is ({value}, {content})");

      var instance = _factory.Get(value, content);
      var a = instance.GetType().GetCustomAttribute<VersionAttribute>();
      Console.WriteLine($"  ==> Actual type {instance.GetType().Name} registered as ({a.Version}, {a.Content})");
    }

    #endregion
  }

  #endregion
}