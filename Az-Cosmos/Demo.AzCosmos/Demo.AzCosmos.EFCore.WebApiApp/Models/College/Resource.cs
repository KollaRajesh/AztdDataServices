using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Demo.AzCosmos.EFCore.WebApiApp.Models.Infrastructure;

namespace Demo.AzCosmos.EFCore.WebApiApp.Models
{ 
    public class Resource //:IKey<int>
    {
        //  [JsonProperty(PropertyName = "id")]
        // public int Id {get;set;}
        public string ResourceName { get; set; }
        public bool IsQualityCheckCompleted { get; set; }
        public DateTime QualityCheckDate { get; set; }
        
        public Resource()
        {
            
        }

    }
}