using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Demo.AzCosmos.EFCore.ConsoleApp.Models;

namespace Demo.AzCosmos.EFCore.ConsoleApp.Services
{
    public static  class CollegeServiceHelper
    {
        
        
        public static College CreateChicagoUniversity()
        {
            return new College()
            {
                Id = 1,
                RegtNumber = 18901,
                Name = "University of Chicago",
                PartitionId = "1890"
            };
        }

        public static Address CreateChicagoUniversityAddress()
        {
            return new Address()
            {
                Apartment = string.Empty,
                BuildingNumber = string.Empty,
                Street = "H. Levi Hall 5801 South Ellis Avenue",
                City = "Chicago",
                State = "IL",
                ZipCode = "60637",
                Country = "USA"
            };
        }

         public static ICollection<Department> CreateDepartmentsForChicagoUniversity()
        {
            return new List<Department>()
            {
                new Department() {Id=1,
                                DeptName="Biological Sciences",
                                DeptHeadName="Michel",
                                DeptStartDate= new DateTime(1894,1,1)},
                new Department() {Id=2,
                                DeptName="Physical Sciences",
                                DeptHeadName="Chris",
                                DeptStartDate= new DateTime(1905,1,1)},
                new Department() {Id=3,
                                DeptName="Physics",
                                DeptHeadName="Anthony",
                                DeptStartDate= new DateTime(1894,1,1)},
                new Department() {Id=4,
                                DeptName="Mathematics",
                                DeptHeadName="Bart",
                                DeptStartDate= new DateTime(1894,1,1)},
                new Department() {Id=5,
                                DeptName="SocialSciences",
                                DeptHeadName="Ben",
                                DeptStartDate= new DateTime(1925,1,1)},
                new Department() {Id=6,
                                DeptName="Law",
                                DeptHeadName="Charlie",
                                DeptStartDate= new DateTime(1926,1,1)},
                new Department() {Id=7,
                                DeptName="Computer Sciences",
                                DeptHeadName="David",
                                DeptStartDate= new DateTime(1970,1,1)},
                new Department() {Id=8,
                                DeptName="Music Sciences",
                                DeptHeadName="Daniel",
                                DeptStartDate= new DateTime(1905,1,1)}                            
            };
        }

        public static College CreateColumbiaUniversity()
        {
            return new College()
            {
                Id = 4,
                RegtNumber = 17541,
                Name = "Columbia University",
                PartitionId = "1754"
            };
        }
        public static Address CreateColumbiaUniversityAddress()
        {
            return new Address()
            {
                Apartment = string.Empty,
                BuildingNumber = string.Empty,
                Street = "116th Street",
                City = "New York City",
                State = "NY",
                ZipCode = "10027",
                Country = "USA"
            };
        }
        public static ICollection<Department> CreateDepartmentsForColumbiaUniversity()
        {
            return new List<Department>()
            {
                new Department() {Id=41,
                                DeptName="Biological Sciences",
                                DeptHeadName="Beven",
                                DeptStartDate= new DateTime(1756,1,1)},
                new Department() {Id=42,
                                DeptName="Physical Sciences",
                                DeptHeadName="Tom",
                                DeptStartDate= new DateTime(1820,1,1)},
                new Department() {Id=43,
                                DeptName="Physics",
                                DeptHeadName="Eric",
                                DeptStartDate= new DateTime(1780,1,1)},
                new Department() {Id=44,
                                DeptName="Mathematics",
                                DeptHeadName="Rob",
                                DeptStartDate= new DateTime(1790,1,1)},
                new Department() {Id=45,
                                DeptName="SocialSciences",
                                DeptHeadName="Ben",
                                DeptStartDate= new DateTime(1915,1,1)},
                new Department() {Id=46,
                                DeptName="Law",
                                DeptHeadName="Anthony",
                                DeptStartDate= new DateTime(1936,1,1)},
                new Department() {Id=47,
                                DeptName="Computer Sciences",
                                DeptHeadName="Edison",
                                DeptStartDate= new DateTime(1975,1,1)},
                new Department() {Id=48,
                                DeptName="Music Sciences",
                                DeptHeadName="James",
                                DeptStartDate= new DateTime(1907,1,1)}                            
            };
        }
        public static College CreateHarvardUniversity()
        {
            return new College()
            {
                Id = 3,
                RegtNumber = 16361,
                Name = "Harvard University",
                PartitionId = "1636"
            };
        }

        public static Address CreateHarvardUniversityAddress()
        {
            return new Address()
            {
                Apartment = string.Empty,
                BuildingNumber = string.Empty,
                Street = "Massachusetts Hall",
                City = "Cambridge",
                State = "MA",
                ZipCode = "02138",
                Country = "USA"
            };
        }

public static ICollection<Department> CreateDepartmentsForHarvardUniversity()
        {
            return new List<Department>()
            {
                new Department() {Id=31,
                                DeptName="Biological Sciences",
                                DeptHeadName="Samuel",
                                DeptStartDate= new DateTime(1756,1,1)},
                new Department() {Id=32,
                                DeptName="Physical Sciences",
                                DeptHeadName="Martin",
                                DeptStartDate= new DateTime(1820,1,1)},
                new Department() {Id=33,
                                DeptName="Physics",
                                DeptHeadName="James",
                                DeptStartDate= new DateTime(1780,1,1)},
                new Department() {Id=34,
                                DeptName="Mathematics",
                                DeptHeadName="Chris",
                                DeptStartDate= new DateTime(1790,1,1)},
                new Department() {Id=35,
                                DeptName="SocialSciences",
                                DeptHeadName="Bart",
                                DeptStartDate= new DateTime(1915,1,1)},
                new Department() {Id=36,
                                DeptName="Law",
                                DeptHeadName="Thomas",
                                DeptStartDate= new DateTime(1936,1,1)},
                new Department() {Id=37,
                                DeptName="Computer Sciences",
                                DeptHeadName="Williams",
                                DeptStartDate= new DateTime(1975,1,1)},
                new Department() {Id=38,
                                DeptName="Music Sciences",
                                DeptHeadName="Kelly",
                                DeptStartDate= new DateTime(1907,1,1)}                            
            };
        }
        public static College CreatePrincetonUniversity()
        {
            return new College()
            {
                Id = 2,
                RegtNumber = 17461,
                PartitionId = "1746"
            };
        }
        public static Address CreatePrincetonUniversityAddress()
        {
            return new Address()
            {
                Apartment = string.Empty,
                BuildingNumber = string.Empty,
                Street = "1 Nassau Hall",
                City = "PrinceTon",
                State = "NJ",
                ZipCode = "08544-0070",
                Country = "USA"
            };
        }

        
public static ICollection<Department> CreateDepartmentsForPrincetonUniversity()
 {
            return new List<Department>()
            {
                new Department() {Id=21,
                                DeptName="Biological Sciences",
                                DeptHeadName="Darwin",
                                DeptStartDate= new DateTime(1786,1,1)},
                new Department() {Id=22,
                                DeptName="Physical Sciences",
                                DeptHeadName="Mercury",
                                DeptStartDate= new DateTime(1790,1,1)},
                new Department() {Id=23,
                                DeptName="Physics",
                                DeptHeadName="Newton",
                                DeptStartDate= new DateTime(1891,1,1)},
                new Department() {Id=24,
                                DeptName="Mathematics",
                                DeptHeadName="Martin",
                                DeptStartDate= new DateTime(1780,1,1)},
                new Department() {Id=25,
                                DeptName="SocialSciences",
                                DeptHeadName="Frederic",
                                DeptStartDate= new DateTime(1815,1,1)},
                new Department() {Id=26,
                                DeptName="Law",
                                DeptHeadName="Philips",
                                DeptStartDate= new DateTime(1850,1,1)},
                new Department() {Id=27,
                                DeptName="Computer Sciences",
                                DeptHeadName="Warner",
                                DeptStartDate= new DateTime(1965,1,1)},
                new Department() {Id=38,
                                DeptName="Music Sciences",
                                DeptHeadName="Marry",
                                DeptStartDate= new DateTime(1840,1,1)}                            
            };
 }

        public static College CreateUniversityOfPennsylvania()
        {
            return new College()
            {
                Id = 6,
                RegtNumber = 17401,
                Name = "University of Pennsylvania",
                PartitionId = "1740"
            };
        }
        public static Address CreateUniversityOfPennsylvaniaAddress()
        {
            return new Address()
            {
                Apartment = string.Empty,
                BuildingNumber = string.Empty,
                Street = "3451 Walnut St",
                City = "Philadelphia",
                State = "PA",
                ZipCode = "19104",
                Country = "USA"
            };
        }

public static ICollection<Department> CreateDepartmentsForUniversityOfPennsylvania()
 {
            return new List<Department>()
            {
                new Department() {Id=61,
                                DeptName="Biological Sciences",
                                DeptHeadName="Martin",
                                DeptStartDate= new DateTime(1786,1,1)},
                new Department() {Id=62,
                                DeptName="Physical Sciences",
                                DeptHeadName="Ben",
                                DeptStartDate= new DateTime(1790,1,1)},
                new Department() {Id=63,
                                DeptName="Physics",
                                DeptHeadName="Rutherford",
                                DeptStartDate= new DateTime(1891,1,1)},
                new Department() {Id=64,
                                DeptName="Mathematics",
                                DeptHeadName="Albert",
                                DeptStartDate= new DateTime(1780,1,1)},
                new Department() {Id=65,
                                DeptName="SocialSciences",
                                DeptHeadName="Alain",
                                DeptStartDate= new DateTime(1815,1,1)},
                new Department() {Id=66,
                                DeptName="Law",
                                DeptHeadName="Samuel",
                                DeptStartDate= new DateTime(1850,1,1)},
                new Department() {Id=67,
                                DeptName="Computer Sciences",
                                DeptHeadName="Gates",
                                DeptStartDate= new DateTime(1965,1,1)},
                new Department() {Id=68,
                                DeptName="Music Sciences",
                                DeptHeadName="Lisa",
                                DeptStartDate= new DateTime(1840,1,1)}                            
            };
 }

        public static College CreateYaleUniversity()
        {
            return new College()
            {
                Id = 5,
                RegtNumber = 17011,
                Name = "Yale University",
                PartitionId = "1890"
            };
        }
        public static Address CreateYaleUniversityAddress()
        {
            return new Address()
            {
                Apartment = string.Empty,
                BuildingNumber = string.Empty,
                Street = "Woodbridge Hall",
                City = "New Haven",
                State = "CT",
                ZipCode = "06520",
                Country = "USA"
            };
        }

      
public static ICollection<Department> CreateDepartmentsForYaleUniversity()
 {
            return new List<Department>()
            {
                new Department() {Id=71,
                                DeptName="Biological Sciences",
                                DeptHeadName="Alex",
                                DeptStartDate= new DateTime(1786,1,1)},
                new Department() {Id=72,
                                DeptName="Physical Sciences",
                                DeptHeadName="Samuel",
                                DeptStartDate= new DateTime(1790,1,1)},
                new Department() {Id=73,
                                DeptName="Physics",
                                DeptHeadName="Bert",
                                DeptStartDate= new DateTime(1891,1,1)},
                new Department() {Id=74,
                                DeptName="Mathematics",
                                DeptHeadName="Alberto",
                                DeptStartDate= new DateTime(1780,1,1)},
                new Department() {Id=75,
                                DeptName="SocialSciences",
                                DeptHeadName="Samuel",
                                DeptStartDate= new DateTime(1815,1,1)},
                new Department() {Id=76,
                                DeptName="Law",
                                DeptHeadName="Michel",
                                DeptStartDate= new DateTime(1850,1,1)},
                new Department() {Id=77,
                                DeptName="Computer Sciences",
                                DeptHeadName="Grambell",
                                DeptStartDate= new DateTime(1965,1,1)},
                new Department() {Id=78,
                                DeptName="Music Sciences",
                                DeptHeadName="Pascal",
                                DeptStartDate= new DateTime(1840,1,1)}                            
            };
 }
     public static  void  WriteItemsToConsole(List<College> colleges)
   {
          colleges.ForEach(c=> {
                             Console.WriteLine($"College Name : {c.Name} \t  Est Date:{c.PartitionId} \t  Address: {c.Address.ToString()}");
                                c.Departments.ToList().ForEach( d => {
                                 Console.WriteLine($"\t Dept Name : {d.DeptName} \t Dept Start :{d.DeptStartDate} \t Head: {d.DeptHeadName}");
                                    d.Courses.ToList().ForEach( crs => {
                                          Console.WriteLine($" \t \t Course Name : {crs.CourseName} \t Course Duration : {crs.CourseDuration} \t Fee: {crs.CourseFee}");
                                        });
                               });

                         });

        }

    }
}