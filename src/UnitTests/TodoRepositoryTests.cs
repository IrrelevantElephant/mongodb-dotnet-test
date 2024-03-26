using Api;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NSubstitute;

namespace UnitTests
{
    public class TodoRepositoryTests
    {
        [Fact]
        public async Task CreateTodo_CallsInsert()
        {
            var mockMongoClient = Substitute.For<IMongoClient>();
            var mockDatabase = Substitute.For<IMongoDatabase>();
            var mockCollection = Substitute.For<IMongoCollection<Todo>>();

            mockMongoClient.GetDatabase(Arg.Any<string>()).Returns(mockDatabase);
            mockDatabase.GetCollection<Todo>(Arg.Any<string>()).Returns(mockCollection);

            var settings = Options.Create(new Settings { MongoConnectionString = string.Empty });
            var sut = new TodoRepository(mockMongoClient, settings);

            var todo = new Todo
            {
                Title = "Feed the cat"
            };

            await sut.InsertAsync(todo);

            await mockCollection.Received().InsertOneAsync(todo, Arg.Any<InsertOneOptions>(), Arg.Any<CancellationToken>());
        }
    }
}