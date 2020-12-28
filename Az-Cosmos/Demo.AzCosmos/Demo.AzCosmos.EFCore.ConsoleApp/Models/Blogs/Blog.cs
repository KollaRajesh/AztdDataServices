using System.Collections.Generic;
using Newtonsoft.Json;
using Demo.AzCosmos.EFCore.ConsoleApp.Models.Infrastructure;

namespace Demo.AzCosmos.EFCore.ConsoleApp.Models
{
    public class Blog:IKey<int>,IPartitionKey
    {
         [JsonProperty(PropertyName = "BlogId")]
         public int Id { get; set; }
         public string BlogTitle { get; set; }
         
        [JsonProperty(PropertyName = "RelatedTopic")]
         public string PartitionId { get; set; }=string.Empty;
         public string BlogUrl { get; set; }
         public ICollection<Post> Posts {get;set;}=new HashSet<Post>();
    }
}
