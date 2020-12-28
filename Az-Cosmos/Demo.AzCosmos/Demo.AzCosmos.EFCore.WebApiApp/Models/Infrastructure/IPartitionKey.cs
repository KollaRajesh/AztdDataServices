namespace Demo.AzCosmos.EFCore.WebApiApp.Models.Infrastructure
{
    public interface IPartitionKey
    {
        string PartitionId {get;set;}
    }
}