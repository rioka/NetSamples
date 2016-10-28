using System.Runtime.Serialization;

namespace IoCComparison.WcfServices.Models {
   
   [DataContract]
   public class BarRequest {

      [DataMember]
      public string TaskName { get; set; }
   }
}