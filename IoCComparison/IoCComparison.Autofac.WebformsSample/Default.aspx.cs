using System;
using System.Web.UI;
using IoCComparison.Core;

namespace IoCComparison.Autofac.WebformsSample {
   public partial class _Default : Page {

      public IService Service { get; set; }
      
      protected void Page_Load(object sender, EventArgs e) {

      }
   }
}