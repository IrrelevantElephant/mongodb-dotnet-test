using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

class Todo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public required string Title { get; set; }
}
