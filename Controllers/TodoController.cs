

using LearnDotnet.Contracts;
using LearnDotnet.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/todos")]
public class TodoController: ControllerBase
{
    private readonly ITodoServices _todoServices;
    
    private readonly ILogger<TodoController> _logger;

    public TodoController(ILogger<TodoController> logger, ITodoServices todoServices)
    {
        _logger = logger;
        _todoServices = todoServices;
    }

    /// <summary>
    /// Creates a new Todo item.
    /// </summary>
    /// <param name="request">The details of the Todo item to be created.</param>
    /// <returns>An IActionResult representing the result of the operation.</returns>
    /// Creates a TodoItem.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>A newly created TodoItem</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Todo
    ///     {
    ///        "id": 1,
    ///        "name": "Item #1",
    ///        "isComplete": true
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _todoServices.CreateTodoAsync(request);
            return Ok(new { message = "Blog post successfully created" });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });

        }
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var todos = await _todoServices.GetAllTodo();

            if (todos == null || !todos.Any())
            {
                return Ok(new { message = "No todo item found" });
            }
        
            return Ok( new { message = "Successfully retrieved all blog posts", data = todos } );

        }
        catch (Exception e)
        {
             return StatusCode(500, new { message = "An error occurred while retrieving all Tood it posts", error = e.Message });
        }
        
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var todo = await _todoServices.GetByIdAsyc(id);

            if (todo == null)
            {
                return NotFound(new { message = $"No Todo item with Id {id} found." });
            }
        
            return Ok( new { message = $"Successfully retrieved blog post with {id} ", data = todo } );

        }
        catch (Exception e)
        {
             return StatusCode(500, new { message = "An error occurred while retrieving todo it post", error = e.Message });
        }
    }
    
    /// <summary>
    /// Delete Todo
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        try
        {
             await _todoServices.DeleteTodoAsync(id);
            //
            // if (todo == null)
            // {
            // return NotFound(new { message = $"No Todo item with Id {id} found." });
            // }
            //
            return Ok( new { message = $"Successfully retrieved blog post with {id} " } );
        }
        
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"An error occurred while deleting todo with {id}", error = e.Message });
        }
        
        
    }
    
    



}