using Api;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

builder.Services.Configure<Settings>(builder.Configuration);

var settings = builder.Configuration.Get<Settings>();

var mongoClient = new MongoClient(settings!.MongoConnectionString);
builder.Services.AddSingleton<IMongoClient>(mongoClient);

builder.Services.AddScoped<ITodoRepository, TodoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
