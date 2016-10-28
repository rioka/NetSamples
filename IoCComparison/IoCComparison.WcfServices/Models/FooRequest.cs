using System.Runtime.Serialization;

namespace IoCComparison.WcfServices.Models {
   
   [DataContract]
   public class FooRequest {

      [DataMember]
      public string Name { get; set; }

      [DataMember]
      public int Age { get; set; }
   }
}