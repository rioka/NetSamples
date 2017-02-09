using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using Fasterflect;

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

    private static readonly Action<SampleService, int> DynamicMethodDelegate = CreateDynamicDelegate();

    private static readonly Func<object, object[], object> DynamicMethodDelegateWithObject = CreateDynamicDelegateWithObject();

    static readonly ConstructorInvoker Ctor = typeof(SampleService).DelegateForCreateInstance(new Type[] { });

    private static readonly MethodInvoker FasterFlectDelegate = typeof(SampleService).DelegateForCallMethod("Run", new[] {typeof(int)});
    
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
      DynamicMethodDelegate.Invoke(instance, i);
    };

    private static readonly Action<int> UsingDynamicMethodWithTypeObject = i => {

      var instance = Activator.CreateInstance<SampleService>();
      DynamicMethodDelegateWithObject.Invoke(instance, new object[] {i});
    };

    private static readonly Action<int> FasterFlect = i => {

      // Ctor() is much faster
      // var instance = Ctor();  
      var instance = Activator.CreateInstance<SampleService>();
      FasterFlectDelegate(instance, i);
    };

    static void Main(string[] args)
    {
      var count = args.Any() ? Convert.ToInt32(args[0]) : 100000;

      TrackExecution(count, DirectCall, "Direct call");
      TrackExecution(count, SimpleReflection, "Simple reflection");
      TrackExecution(count, UsingCreateDelegate, "Creating a delegate");
      TrackExecution(count, UsingDynamicMethod, "Creating DynamicMethod");
      //TrackExecution(count, UsingDynamicMethodWithTypeObject, "Creating DynamicMethod with type object");
      TrackExecution(count, FasterFlect, "Using Fasterflect");
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

      // pushing parameters in the stack for the call
      // First the instance
      // then true method parameters
      for (var i = 0; i < ParameterTypes2.Length; i++)
      {
        gen.Emit(OpCodes.Ldarg, i);
      }

      gen.EmitCall(OpCodes.Call, MethodInfo, null);
      gen.Emit(OpCodes.Ret);
      
      return (Action<SampleService, int>) dm.CreateDelegate(typeof(Action<SampleService, int>));
    }

    private static Func<object, object[], object> CreateDynamicDelegateWithObject()
    {
      var dm = new DynamicMethod(typeof(SampleService) + ".Run", typeof(object), new Type[] { typeof(object), typeof(object[]) });
      var gen = dm.GetILGenerator();

      // pushing parameters in the stack for the call
      // First the instance
      // then true method parameters
      // update to handle an array of objects instead 
      //for (var i = 0; i < ParameterTypes2.Length; i++)
      //{
      //  gen.Emit(OpCodes.Ldarg, i);
      //}

      gen.EmitCall(OpCodes.Call, MethodInfo, null);
      gen.Emit(OpCodes.Ret);

      return (Func<object, object[], object>) dm.CreateDelegate(typeof(Func<object, object[], object>));
    }
  }
}