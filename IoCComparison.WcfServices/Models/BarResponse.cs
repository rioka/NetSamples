using System.Runtime.Serialization;

namespace IoCComparison.WcfServices.Models {
   
   [DataContract]
   public class BarResponse {

      [DataMember]
      public string Code { get; set; }
   }
}