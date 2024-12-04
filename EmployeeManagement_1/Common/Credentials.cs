namespace EmployeeManagement_1.Common
{
    public class Credentials
    {
        public static readonly string cosmosUrl = Environment.GetEnvironmentVariable("CosmosUrl");
        public static readonly string authToken = Environment.GetEnvironmentVariable("authkey");
        public static readonly string databaseName = Environment.GetEnvironmentVariable("database");
        public static readonly string containerName = Environment.GetEnvironmentVariable("container");
        
        public static readonly string leadDocumentType = "lead";
        public static readonly string memberDocumentType = "user";
        public static readonly string taskDocumentType = "taskSheet";
        public static readonly string createdBy = "Siddharth";
        public static readonly string createdByName = "Mishra";
    }
}
