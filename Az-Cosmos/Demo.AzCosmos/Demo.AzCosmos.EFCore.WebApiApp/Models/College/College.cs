using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Demo.AzCosmos.EFCore.WebApiApp.Models.Infrastructure;

namespace Demo.AzCosmos.EFCore.WebApiApp.Models
{
    public class College:IKey<int>,IPartitionKey
    {

       [JsonProperty(PropertyName = "id")]
        public int Id {get;set;}
        public int RegtNumber { get; set; }
        public string Name { get; set; }

        [JsonProperty(PropertyName = "EstYear")]
        public string PartitionId { get; set; }        
        public Address  Address { get; set; }= new Address();
        public  ICollection<Department> Departments {get;set;}= new HashSet<Department>();
        public  ICollection<Resource> Resources {get;set;}= new HashSet<Resource>();

        public College()
        {

        } 

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}