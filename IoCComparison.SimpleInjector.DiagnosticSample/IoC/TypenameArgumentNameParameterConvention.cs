using System;
using System.Collections.Generic;
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

      static readonly Type[] AllowedTypes = {
         typeof(int),
         typeof(string)
      };

      public bool CanResolve(InjectionTargetInfo target, Type injectedInto) {

         var type = target.TargetType;
         var resolvable = AllowedTypes.Contains(type);

         if (resolvable) {
            VerifyAppSettings(target.Name, injectedInto.Name);
         }

         return resolvable;
      }

      private void VerifyAppSettings(string name, string injectedIntoTypeName) {

         var key = GetKey(injectedIntoTypeName, name);
         if (string.IsNullOrWhiteSpace(GetAppSetting(name, injectedIntoTypeName))) {
            throw new ActivationException("No application setting with key '" + key + "' could be found in the " +
                                          "application's configuration file.");
         }
      }

      public Expression BuildExpression(InjectionConsumerInfo consumer) {
         var value = GetAppSetting(consumer.Target.Name, consumer.ImplementationType.Name);

         return consumer.Target.TargetType != typeof(string)
             ? Expression.Constant(Convert.ChangeType(value, consumer.Target.TargetType))
             : Expression.Constant(value, typeof(string));
      }

      static string GetAppSetting(string name, string injectedIntoTypeName) {

         var key = GetKey(injectedIntoTypeName, name);
         return ConfigurationManager.AppSettings[key];
      }

      static string GetKey(string injectedIntoTypeName, string name) {
         return string.Format("{0}.{1}", injectedIntoTypeName, name);
      }
   }
}
