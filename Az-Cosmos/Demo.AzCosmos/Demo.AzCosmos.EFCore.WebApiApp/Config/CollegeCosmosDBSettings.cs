
namespace Demo.AzCosmos.EFCore.WebApiApp.Config
{
    public class CollegeCosmosDBSettings:CosmosDBSettings
    {
        public const string Settings=nameof(CollegeCosmosDBSettings);
        public string DatabaseId {get;set;} = "Collegedb";
        public string ContainerId {get;set;} = "Colleges";

    }
}