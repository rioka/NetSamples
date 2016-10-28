
using System;

namespace UsingFactories.Core.FactoryDelegate {
   
   public class Uploader {
      
      private readonly TranslatorFactory _factory;

      //public Uploader(TranslatorFactory factory) {
      public Uploader(Func<int, ITranslator> factory) {
         
         // we cannot use directly a delegate and get a factory implemented by Ninject for it
         // but we can have a function implemented instead, and create a delegate from it
         // so that we can quickly switch to Autofac just changing the .ctor signatore to 
         // accept a delegate and get it implemented automatically by Autofac
         _factory = x => factory(x);
      }

      public void Upload(string file) {

         var translator = _factory(10);
         // ...
         translator.Translate("Done");
      }
   }
}
