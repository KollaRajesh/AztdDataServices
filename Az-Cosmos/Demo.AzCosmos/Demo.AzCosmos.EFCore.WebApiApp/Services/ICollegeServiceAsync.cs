using System.Threading.Tasks;
using System.Collections.Generic;
using Demo.AzCosmos.EFCore.WebApiApp.Models;

namespace Demo.AzCosmos.EFCore.WebApiApp.Services
{
    public interface ICollegeServiceAsync
    {
        Task CreateSampleCollegesAsSync();
        Task DeleteCollegeDatabaseIfExists();
        Task<College> GetByIdAsync(int collegeId);
        Task<College> GetByNameAsync(string collegeName);
        Task<List<College>> GetColleges();
        Task UpdateAddressforCollegesAsync();
        Task UpdateDepartmentsforCollegesAsync();
        Task WriteItemsToConsoleAsync();
    }
}