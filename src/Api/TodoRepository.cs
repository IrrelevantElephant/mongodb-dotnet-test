using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api;

public class TodoRepository : ITodoRepository
{
    private readonly IMongoClient _mongoClient;
    private readonly IOptions<Settings> _settings;

    public TodoRepository(IMongoClient mongoClient, IOptions<Settings> settings)
    {
        _mongoClient = mongoClient;
        _settings = settings;
    }

    public async Task InsertAsync(Todo todo)
    {
        var database = _mongoClient.GetDatabase(_settings.Value.DatabaseName);
        var collection = database.GetCollection<Todo>(_settings.Value.CollectionName);
        await collection.InsertOneAsync(todo);
    }

    public async Task<IEnumerable<Todo>> GetAsync()
    {
        var database = _mongoClient.GetDatabase(_settings.Value.DatabaseName);
        var collection = database.GetCollection<Todo>(_settings.Value.CollectionName);
        var todos = await collection.FindAsync(new BsonDocument());
        return await todos.ToListAsync();
    }
}
