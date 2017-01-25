using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Performances
{
  class Program
  {
    private static readonly MethodInfo MethodInfo = typeof(SampleService).GetMethod("Run");

    private static readonly Type[] ParameterTypes =
      MethodInfo.GetParameters().Select(p => p.ParameterType).ToArray();
    
    private static readonly Type DelegateType = Expression.GetActionType(ParameterTypes);

    private static readonly Type[] ParameterTypes2 = new[] {typeof(SampleService)}
      .Union(ParameterTypes)
      .ToArray();

    private static Action<SampleService, int> DynamicMethodDelegate = CreateDynamicDelegate();

    private static readonly Action<int> DirectCall = i => {
      
      var instance = new SampleService();
      instance.Run(1);
    };

    private static readonly Action<int> SimpleReflection = i => {

      var instance = Activator.CreateInstance<SampleService>();
      MethodInfo.Invoke(instance, new object[] {1});
    };

    private static readonly Action<int> UsingCreateDelegate = i => {

      var instance = Activator.CreateInstance<SampleService>();
      var d = (Action<int>) Delegate.CreateDelegate(DelegateType, instance, MethodInfo);
      d(i);
    };

    private static readonly Action<int> UsingDynamicMethod = i => {

      var instance = Activator.CreateInstance<SampleService>();
      DynamicMethodDelegate.Invoke(instance, 1);
    };

    static void Main(string[] args)
    {
      var count = args.Any() ? Convert.ToInt32(args[0]) : 100000;

      TrackExecution(count, DirectCall, "Direct call");
      TrackExecution(count, SimpleReflection, "Simple reflection");
      TrackExecution(count, UsingCreateDelegate, "Creating a delegate");
      TrackExecution(count, UsingDynamicMethod, "Creating DynamicMethod");
    }

    private static void TrackExecution(int count, Action<int> action, string mode)
    {
      var sw = new Stopwatch();
      sw.Start();

      for (var i = 0; i < count; i++)
      {
        action(i);
      }

      sw.Stop();
      Console.WriteLine("{1} {0:#,##0}", sw.ElapsedMilliseconds, mode);
    }

    private static Action<SampleService, int> CreateDynamicDelegate()
    {
      var dm = new DynamicMethod(typeof(SampleService) + ".Run", MethodInfo.ReturnType, ParameterTypes2);
      var gen = dm.GetILGenerator();

      // SampleService instance 
      // parameters, including the calling instance itself at index 0
      for (var i = 0; i < ParameterTypes2.Length; i++)
      {
        gen.Emit(OpCodes.Ldarg, i);
      }

      gen.EmitCall(OpCodes.Call, MethodInfo, null);
      gen.Emit(OpCodes.Ret);
      
      return (Action<SampleService, int>) dm.CreateDelegate(typeof(Action<SampleService, int>));
    }
  }
}