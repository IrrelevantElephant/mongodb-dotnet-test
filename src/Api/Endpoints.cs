using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPost("/todo", (IMongoClient mongoClient, IOptions<Settings> settings, CreateTodoRequest todo) =>
        {
            var newTodo = new Todo
            {
                Title = todo.Title
            };

            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            var collection = database.GetCollection<Todo>(settings.Value.CollectionName);
            collection.InsertOne(newTodo);
            return TypedResults.Created($"/todo/{newTodo.Id}", newTodo);
        }).WithName("CreateTodo").WithOpenApi();

        app.MapGet("/todo", (IMongoClient mongoClient, IOptions<Settings> settings) =>
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            var collection = database.GetCollection<Todo>(settings.Value.CollectionName);
            var todos = collection.Find(new BsonDocument()).ToList();
            return TypedResults.Ok(todos);
        }).WithName("GetTodos").WithOpenApi();
    }
}