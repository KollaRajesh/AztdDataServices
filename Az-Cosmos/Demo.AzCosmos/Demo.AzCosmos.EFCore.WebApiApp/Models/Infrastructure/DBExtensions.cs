using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;


namespace Demo.AzCosmos.EFCore.WebApiApp.Models.Infrastructure
{   
 public static class DBExtensions
 {

    public static void UpdateAndDetachLocalIfAny<T>(this DbContext context, T currentEntity, int entryId) 
        where T : class,IKey<int>  
    {
        var local = context.Set<T>()
            .Local
            .FirstOrDefault(entry => entry.Id.ToString().Equals(entryId));
        if (local!=null)
        {
            context.Entry(local).State = EntityState.Detached;
        }
        context.Update(currentEntity);
        context.Entry(currentEntity).State = EntityState.Modified;
    }

    public static void UpdateAfterDetachLocalIfAny<T>(this DbContext context, T currentEntity, int entryId,string partitionId) 
            where T : class,IKey<int>,IPartitionKey 
    {
        var local = context.Set<T>()
            .Local
            .FirstOrDefault(entry => entry.Id.Equals(entryId) && entry.PartitionId.ToString()
            .Equals(partitionId));
        if (local!=null)
        {
            context.Entry(local).State = EntityState.Detached;
        }
        context.Update(currentEntity);
        context.Entry(currentEntity).State = EntityState.Modified;
    }
     
     public static void DeleteAfterDetachLocalIfAny<T>(this DbContext context, T currentEntity, int entryId,string partitionId) 
            where T : class,IKey<int>,IPartitionKey 
    {
        var local = context.Set<T>()
            .Local
            .FirstOrDefault(entry => entry.Id.Equals(entryId) && entry.PartitionId.ToString()
            .Equals(partitionId));
        if (local!=null)
        {
            context.Entry(local).State = EntityState.Detached;
        }
        context.Update(currentEntity);
        context.Entry(currentEntity).State = EntityState.Deleted;
    }

    
    }
}