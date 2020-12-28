namespace Demo.AzCosmos.EFCore.ConsoleApp.Models.Infrastructure
{
    public interface IKey<T>
    {
          T Id {get;set;}
    }
}