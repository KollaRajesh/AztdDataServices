using System.Collections.Generic;
using Newtonsoft.Json;

namespace Demo.AzCosmos.EFCore.WebApiApp.Models
{
    public class Lab//:IKey<int>
    {
        //  [JsonProperty(PropertyName = "id")]
        // public int Id {get;set;}
        public string LabName { get; set; }
        public string InChargPerson { get; set; }
        public int DurationInHourss { get; set; }
        public ICollection<Resource> Infrastructures { get; set; } =new HashSet<Resource>();
        public Lab()
        {
            
        }
       
    }
}