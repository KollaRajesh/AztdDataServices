using System.Collections.Generic;
using Demo.AzCosmos.EFCore.WebApiApp.Models;

namespace Demo.AzCosmos.EFCore.WebApiApp.Services
{
    public interface ICollegeService
    {
        void CreateSampleColleges();
        void DeleteCollegeDatabaseIfExists();
        List<College> GetColleges();
        void UpdateAddressforColleges();
        void UpdateDepartmentsforColleges();
        void WriteItemsToConsole();
    }
}