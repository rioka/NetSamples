using System;

namespace CurryingSamples.Generalization.Extensions
{
  public static class FuncExtensions
  {
    #region Currying
    
    /// <summary>
    /// Given a function with an argument of type T, we return a parameterless
    /// function returning a function returning the result of the given type
    /// </summary>
    /// <typeparam name="T">Type of the parameter</typeparam>
    /// <typeparam name="TResult">Type of the result</typeparam>
    /// <param name="function">The function to curry</param>
    /// <returns>A parameterless function evaluating a new function with 1 parameter evaluating the source function</returns>
    public static Func<Func<T, TResult>> Curry<T, TResult>(this Func<T, TResult> function)
    {
      // src arg => result
      // to () => arg => result
      return () => function;
    }

    /// <summary>
    /// Given a function with two arguments, we return a function with the first argument
    /// returning a function with the second argument returning the result of the given type
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter</typeparam>
    /// <typeparam name="T2">Type of the second parameter</typeparam>
    /// <typeparam name="TResult">Type of the result</typeparam>
    /// <param name="function">The function to curry</param>
    /// <returns>A function with one parameter returning the a function with the second parameter evaluating the original function</returns>
    public static Func<T1, Func<T2, TResult>> Curry<T1, T2, TResult>(this Func<T1, T2, TResult> function)
    {
      // src (arg1, arg2) => result
      // to arg1 => arg2 => result
      return arg1 => arg2 => function(arg1, arg2);
    }

    /// <summary>
    /// Given a function with three arguments, we return a function with the first argument
    /// returning a function with the second argument returning a function with the third argument
    /// returning the result of the given type
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter</typeparam>
    /// <typeparam name="T2">Type of the second parameter</typeparam>
    /// <typeparam name="T3">Type of the third parameter</typeparam>
    /// <typeparam name="TResult">Type of the result</typeparam>
    /// <param name="function">The function to curry</param>
    /// <returns>A function with one parameter returning the a function with the second parameter 
    /// returning the a function with the third parameter evaluating the original function</returns>
    public static Func<T1, Func<T2, Func<T3, TResult>>> Curry<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> function)
    {
      // src (arg1, arg2, arg3) => result
      // to arg1 => arg2 => arg3 => result
      return arg1 => arg2 => arg3 => function(arg1, arg2, arg3);
    }

    /// <summary>
    /// Given a function with three arguments, we return a function with the first argument
    /// returning a function with the second argument returning a function with the third argument
    /// returning a function with the forth argument returning the result of the given type
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter</typeparam>
    /// <typeparam name="T2">Type of the second parameter</typeparam>
    /// <typeparam name="T3">Type of the third parameter</typeparam>
    /// <typeparam name="T4">Type of the forth parameter</typeparam>
    /// <typeparam name="TResult">Type of the result</typeparam>
    /// <param name="function">The function to curry</param>
    /// <returns>A function with one parameter returning the a function with the second parameter 
    /// returning the a function with the third parameter returning the a function with the forth parameter 
    /// evaluating the original function</returns>
    public static Func<T1, Func<T2, Func<T3, Func<T4, TResult>>>> Curry<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> function)
    {
      // src (arg1, arg2, arg3, arg4) => result
      // to arg1 => arg2 => arg3 => arg4 => result
      return arg1 => arg2 => arg3 => arg4 => function(arg1, arg2, arg3, arg4);
    }

    #endregion

    #region Partial

    /// <summary>
    /// Given a function with a parameter and the value for it, returns a parameterless function
    /// which evaluates the original function for the given value
    /// </summary>
    /// <typeparam name="T">Type of the parameter</typeparam>
    /// <typeparam name="TResult">Type of the result</typeparam>
    /// <param name="function">The function to partially apply</param>
    /// <param name="arg">Value for the function</param>
    /// <returns>A function returning the original function evaluated for the given value</returns>
    public static Func<TResult> Partial<T, TResult>(this Func<T, TResult> function, T arg)
    {
      // src arg => result
      // to () => function(arg)
      return () => function(arg);
    }

    /// <summary>
    /// Given a function with two parameters and the value for the first one, returns a function with one parameter
    /// which, when called, evaluates the original function for the given values
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter</typeparam>
    /// <typeparam name="T2">Type of the second parameter</typeparam>
    /// <typeparam name="TResult">Type of the result</typeparam>
    /// <param name="function">The function to partially apply</param>
    /// <param name="arg1">Value for the first argument</param>
    /// <returns>A function with one parameters which, when evaluated, returnd the value of the original function for the given values</returns>
    public static Func<T2, TResult> Partial<T1, T2, TResult>(this Func<T1, T2, TResult> function, T1 arg1)
    {
      // src (arg1, arg2) => result
      // to (arg2) => function(arg1, arg2)
      return arg2 => function(arg1, arg2);
    }

    /// <summary>
    /// Given a function with three parameters and the value for the first one, returns a function with two parameters
    /// which, when called, evaluates the original function for the given values
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter</typeparam>
    /// <typeparam name="T2">Type of the second parameter</typeparam>
    /// <typeparam name="T3">Type of the third parameter</typeparam>
    /// <typeparam name="TResult">Type of the result</typeparam>
    /// <param name="function">The function to partially apply</param>
    /// <param name="arg1">Value for the first argument</param>
    /// <returns>A function with two parameters which, when evaluated, returnd the value of the original function for the given values</returns>
    public static Func<T2, T3, TResult> Partial<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> function, T1 arg1)
    {
      // src (arg1, arg2, arg3) => result
      // to (arg2, arg3) => function(arg1, arg2, arg3)
      return (arg2, arg3) => function(arg1, arg2, arg3);
    }

    #endregion
  }
}
