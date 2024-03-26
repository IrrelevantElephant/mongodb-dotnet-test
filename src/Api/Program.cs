using Api;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

var settings = new Settings();
builder.Configuration.Bind(settings);

var mongoClient = new MongoClient(settings.MongoConnectionString);
builder.Services.AddSingleton<IMongoClient>(mongoClient);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();

class Settings
{
    public string MongoConnectionString { get; set; }
    public string DatabaseName = "TodoDb";
    public string CollectionName = "TodoCollection";
}