using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.AzCosmos.EFCore.ConsoleApp.Models.Infrastructure;

namespace Demo.AzCosmos.EFCore.ConsoleApp.Repositories.Infrastructure
{
    public interface IRepositoryAsync<T>
            where T : class,IKey<int>,IPartitionKey 
    {
       Task<bool>  IsExists(int entityId);
        Task<bool> IsExists(int entityId, string partitionKey);
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(int entityId);
        Task<T> GetByIdAsync(int entityId, string partitionKey);
        Task<int> SaveChangesAsync(T Entity);
        Task<List<int>> SaveChangesAsync(ICollection<T> Entities);
        Task CreateDatabaseIfNotExistsAsync();
        Task  DeleteDatabaseAsync();
        Task DeleteEntityAsync(T entity);
    }
}