namespace Demo.AzCosmos.EFCore.WebApiApp.Models.Infrastructure
{
    public interface IKey<T>
    {
          T Id {get;set;}
    }
}