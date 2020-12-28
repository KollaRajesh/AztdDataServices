
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Demo.AzCosmos.EFCore.ConsoleApp.Models
{
    public class Course //:IKey<int>
    {
    //    [JsonProperty(PropertyName = "id")]
    //     public int Id {get;set;}
        public string CourseName { get; set; }
        public int CourseDuration { get; set; }
        public decimal CourseFee { get; set; }
        // public ICollection<Theory> Theories { get; set; } =new HashSet<Theory>();
        // public ICollection<Lab> Labs { get; set; } =new HashSet<Lab>();
        public Course()
        {
                      
        }
    }
}