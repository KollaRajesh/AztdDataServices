using System.Collections.Generic;
using Newtonsoft.Json;

namespace Demo.AzCosmos.EFCore.WebApiApp.Models
{
    public class Theory  //:IKey<int>
    {
        // [JsonProperty(PropertyName = "id")]
        // public int Id {get;set;}
        public string TheoryName { get; set; }
        public int DurationInHours { get; set; }
        public string FacultyPerson { get; set; }
        public decimal Fee { get; set; }
        public Theory()
        {
            
        }
         
    }
}