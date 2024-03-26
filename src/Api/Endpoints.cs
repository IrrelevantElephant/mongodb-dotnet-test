namespace Api;

public static class Endpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/todo", async (ITodoRepository todoRepository, CreateTodoRequest todo) =>
        {
            var newTodo = new Todo
            {
                Title = todo.Title
            };

            await todoRepository.InsertAsync(newTodo);

            return TypedResults.Created($"/todo/{newTodo.Id}", newTodo);
        }).WithName("CreateTodo").WithOpenApi();

        app.MapGet("/todo", async (ITodoRepository todoRepository) =>
        {
            var todos = await todoRepository.GetAsync();
            return TypedResults.Ok(todos);
        }).WithName("GetTodos").WithOpenApi();
    }
}