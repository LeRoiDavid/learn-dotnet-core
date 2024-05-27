using LearnDotnet.Contracts;
using LearnDotnet.Models;

namespace LearnDotnet.Interfaces;

public interface ITodoServices
{
    Task<IEnumerable<Todo>> GetAllTodo();

    Task<Todo> GetByIdAsyc(Guid id);

    Task CreateTodoAsync(CreateTodoRequest todoRequest);
    
    Task UpdateTodoAsync(Guid id, UpdateTodoRequest todoRequest);
    
    Task DeleteTodoAsync(Guid id);
    
}