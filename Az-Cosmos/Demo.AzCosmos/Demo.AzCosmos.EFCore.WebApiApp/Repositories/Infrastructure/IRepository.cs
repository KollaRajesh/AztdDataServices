using System.Collections.Generic;
using Demo.AzCosmos.EFCore.WebApiApp.Models.Infrastructure;

namespace Demo.AzCosmos.EFCore.WebApiApp.Repositories.Infrastructure 
{
    public interface IRepository<T>
            where T : class,IKey<int>,IPartitionKey 
    {
        bool  IsExists(int entityId);
        bool IsExists(int entityId, string partitionKey);
        ICollection<T> GetAll();
        T GetById(int entityId);
        T GetById(int entityId, string partitionKey);
        int SaveChanges(T Entity);
        IList<int> SaveChanges(ICollection<T> Entities);
        void CreateDatabaseIfNotExists();
        void  DeleteDatabase();
         void DeleteEntity(T entity);
    }
}