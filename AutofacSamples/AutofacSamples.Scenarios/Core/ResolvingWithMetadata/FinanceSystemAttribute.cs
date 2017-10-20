using System;
using System.Collections.Generic;

namespace AutofacSamples.Scenarios.Core.ResolvingWithMetadata {

   public enum FinanceSystem {

      Dds = 1,
      Sbms = 2,
      Opale = 3
   }

   public class FinanceSystemAttribute : Attribute {

      private FinanceSystem[] _financeSystems;

      public IEnumerable<FinanceSystem> Systems { get { return _financeSystems; } }

      public FinanceSystemAttribute(params FinanceSystem[] financeSystems) {

         _financeSystems = financeSystems;
      }
   }
}