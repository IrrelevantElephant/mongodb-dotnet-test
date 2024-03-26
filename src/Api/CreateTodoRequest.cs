namespace Api;

public record CreateTodoRequest
{
    public required string Title { get; set; }
}

