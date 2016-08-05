using System;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using SimpleInjector;

namespace IoCComparison.SimpleInjector.DiagnosticSample.IoC {

   /// <summary>
   /// Resolve primitive types in constructors searching for a matching 
   /// entry Typename.argumentName in AppSettings
   /// </summary>
   internal class TypenameArgumentNameParameterConvention : IParameterConvention {

      #region Data

      /// <summary>
      /// List of types to be resolved by this convention
      /// </summary>
      static readonly Type[] AllowedTypes = {
         typeof(int),
         typeof(string)
      };

      #endregion

      #region IParameterConvention

      /// <summary>
      /// Check if a dependency can be resolved (i  less dependencies)
      /// </summary>
      /// <param name="target"></param>
      /// <param name="injectedInto"></param>
      /// <returns>True if the exception canl be resolved</returns>
      public bool CanResolve(InjectionTargetInfo target, Type injectedInto) {

         // if the type can be handled...
         var resolvable = AllowedTypes.Contains(target.TargetType);

         if (resolvable) {
            // ... then try a matching entry in the application configuration file
            VerifyAppSettings(target.Name, injectedInto.Name);
         }

         return resolvable;
      }

      /// <summary>
      /// Get the expression for the constructor
      /// </summary>
      /// <param name="consumer">Instance being created</param>
      /// <returns>The expression for the dependency</returns>
      public Expression BuildExpression(InjectionConsumerInfo consumer) {
         var value = GetAppSetting(consumer.Target.Name, consumer.ImplementationType.Name);

         return consumer.Target.TargetType != typeof(string)
             ? Expression.Constant(Convert.ChangeType(value, consumer.Target.TargetType))
             : Expression.Constant(value, typeof(string));
      }

      #endregion

      #region Internals

      /// <summary>
      /// Get the value from the appSettings
      /// </summary>
      /// <param name="name">Name of the parameter passed to the constructor</param>
      /// <param name="injectedIntoTypeName">Type into which the parameter is to be injected</param>
      /// <returns>The value for the matching key in appSettings</returns>
      static string GetAppSetting(string name, string injectedIntoTypeName) {

         var key = GetKey(injectedIntoTypeName, name);
         return ConfigurationManager.AppSettings[key];
      }

      static string GetKey(string injectedIntoTypeName, string name) {
         return string.Format("{0}.{1}", injectedIntoTypeName, name);
      }

      /// <summary>
      /// Check if the parameter can be resolved by this convention
      /// </summary>
      /// <param name="name">Name of the parameter passed to the constructor</param>
      /// <param name="injectedIntoTypeName">Type into which the parameter is to be injected</param>
      private void VerifyAppSettings(string name, string injectedIntoTypeName) {

         var key = GetKey(injectedIntoTypeName, name);
         if (string.IsNullOrWhiteSpace(GetAppSetting(name, injectedIntoTypeName))) {
            throw new ActivationException("No application setting with key '" + key + "' could be found in the " +
                                          "application's configuration file.");
         }
      }

      #endregion
   }
}
