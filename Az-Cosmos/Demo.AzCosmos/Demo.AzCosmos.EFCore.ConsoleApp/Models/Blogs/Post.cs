using Newtonsoft.Json;
using Demo.AzCosmos.EFCore.ConsoleApp.Models.Infrastructure;

namespace Demo.AzCosmos.EFCore.ConsoleApp.Models
{
    public class Post:IKey<string>,IPartitionKey
    {
        [JsonProperty(PropertyName = "PostId")]
         public string Id { get; set; }
         public string Title { get; set; }

         [JsonProperty(PropertyName = "RelatedTopic")]
         public string PartitionId { get; set; }=string.Empty;
    }
}
