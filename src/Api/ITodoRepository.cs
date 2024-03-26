
namespace Api
{
    public interface ITodoRepository
    {
        Task InsertAsync(Todo todo);
        Task<IEnumerable<Todo>> GetAsync();
    }
}