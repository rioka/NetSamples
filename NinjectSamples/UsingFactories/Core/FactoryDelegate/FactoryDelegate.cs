using System;

namespace UsingFactories.Core.FactoryDelegate {
   
   public delegate ITranslator TranslatorFactory(int maxErrors);
}
