namespace Demo.AzCosmos.EFCore.ConsoleApp.Config
{ 
    public class BlogsCosmosDBSettings :CosmosDBSettings
    {
        public const string Settings=nameof(BlogsCosmosDBSettings);
        public string DatabaseId {get;set;} = "Blogsdb";
        public string ContainerId {get;set;} = "Blogs";
    }
}