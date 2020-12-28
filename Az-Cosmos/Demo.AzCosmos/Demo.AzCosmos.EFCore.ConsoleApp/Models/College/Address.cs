using Newtonsoft.Json;

namespace Demo.AzCosmos.EFCore.ConsoleApp.Models
{
    public class Address
    {
       
        public string  Apartment { get; set; }
        public string  BuildingNumber { get; set; }
        public string  Street { get; set; }
        public string  City { get; set; }
        public string  State { get; set; }
        public string  ZipCode { get; set; }
        public string  Country { get; set; }
       public Address() {}

         public override string ToString()
        {
            return $"{Apartment} {BuildingNumber} {Street} {City} {State} {ZipCode} ";
        }
    }
}