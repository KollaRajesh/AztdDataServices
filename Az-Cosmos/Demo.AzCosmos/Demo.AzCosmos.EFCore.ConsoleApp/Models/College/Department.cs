using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Demo.AzCosmos.EFCore.ConsoleApp.Models.Infrastructure;

namespace Demo.AzCosmos.EFCore.ConsoleApp.Models
{
    public class Department :IKey<int>
    {
      [JsonProperty(PropertyName = "id")]
        public int Id {get;set;}
        public string DeptName { get; set; }
        public DateTime DeptStartDate { get; set; }
        public string  DeptHeadName { get; set; }
        public ICollection<Course> Courses { get; set; } =new HashSet<Course>();
       public Department()
       {
           
       } 
    }
}