using System.Runtime.Serialization;

namespace IoCComparison.WcfServices.Models {
   
   [DataContract]
   public class FooResponse {

      [DataMember]
      public string Code { get; set; }

      [DataMember]
      public string Message { get; set; }
   }
}