public class Settings
{
    public required string MongoConnectionString { get; set; }
    public string DatabaseName = "TodoDb";
    public string CollectionName = "TodoCollection";
}