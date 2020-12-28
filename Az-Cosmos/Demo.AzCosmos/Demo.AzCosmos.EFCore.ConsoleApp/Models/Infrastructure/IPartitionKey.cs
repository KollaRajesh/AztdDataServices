namespace Demo.AzCosmos.EFCore.ConsoleApp.Models.Infrastructure
{
    public interface IPartitionKey
    {
        string PartitionId {get;set;}
    }
}