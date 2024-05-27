using AutoMapper;
using LearnDotnet.AppDataContext;
using LearnDotnet.Contracts;
using LearnDotnet.Interfaces;
using LearnDotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnDotnet.Services;

public class TodoServices: ITodoServices
{
    private readonly TodoDbContext _dbContext;

    private readonly ILogger<TodoServices> _logger;

    private readonly IMapper _mapper;
    
    public TodoServices(TodoDbContext dbContext, ILogger<TodoServices> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<Todo>> GetAllTodo()
    {
        var todos = await _dbContext.Todos.ToListAsync();
        if (todos == null)
        {
            throw new Exception(" No Todo items found");
        }
        return todos;
    }

    public async Task<Todo> GetByIdAsyc(Guid id)
    {
        var todo = await _dbContext.Todos.FindAsync(id);
        if (todo == null)
        {
            throw new Exception(" No Todo items found with id = "+id);
        }

        return todo;
    }

    public async Task CreateTodoAsync(CreateTodoRequest todoRequest)
    {
        try
        {
            
            var todo = _mapper.Map<Todo>(todoRequest);
            _logger.LogInformation(todo.ToString());
            todo.CreateAt = DateTime.UtcNow;
            _dbContext.Todos.Add(todo);
            await _dbContext.SaveChangesAsync();

        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating the Todo item.");
            throw new Exception("An error occurred while creating the Todo item.");
        } 
    }

    public async Task UpdateTodoAsync(Guid id, UpdateTodoRequest todoRequest)
    {
        var todo = await _dbContext.Todos.FindAsync(id);
        if (todo == null)
        {
            throw new Exception(" No Todo items found with id = " + id);
        }

        _mapper.Map(todoRequest, todo);
        todo.UpdatedAt = DateTime.Now;
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task DeleteTodoAsync(Guid id)
    {
        var todo = await _dbContext.Todos.FindAsync(id);
        if (todo == null)
        {
            throw new Exception(" No Todo items found with id = " + id);
        }

        _dbContext.Todos.Remove(todo);
        await _dbContext.SaveChangesAsync();
    }
}