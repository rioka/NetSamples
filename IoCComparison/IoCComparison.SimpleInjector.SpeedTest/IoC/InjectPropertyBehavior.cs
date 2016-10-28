using System;
using System.Reflection;
using SimpleInjector;
using SimpleInjector.Advanced;

namespace IoCComparison.SimpleInjector.SpeedTest.IoC {
   
   public class InjectPropertyBehavior : IPropertySelectionBehavior {
      
      private readonly Container _container;

      public InjectPropertyBehavior(Container container) {
         _container = container;
      }

      #region IPropertySelectionBehavior

      public bool SelectProperty(Type serviceType, PropertyInfo propertyInfo) {

         return IsInjectable(propertyInfo) && IsRegistered(propertyInfo);
      }

      #endregion

      #region Internals

      bool IsInjectable(PropertyInfo prop) {
         var setMethod = prop.GetSetMethod(false);
         return setMethod != null && prop.CanWrite && !setMethod.IsStatic;
      }

      bool IsRegistered(PropertyInfo prop) {
         return _container.GetRegistration(prop.PropertyType) != null;
      }
      
      #endregion
   }
}