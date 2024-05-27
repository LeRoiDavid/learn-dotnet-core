using System.ComponentModel.DataAnnotations;

namespace LearnDotnet.Contracts;

public class UpdateTodoRequest
{
    [StringLength(100)]
    public String Title { get; set; }

    [StringLength(500)]
    public String Description { get; set; }

    public bool? IsComplete { get; set; }

    public DateTime? DueDate { get; set; }
    
    [Range(1,5, ErrorMessage = "La valeur de la priorité doit etre comprise en 1 et 5")]
    public int? Priority { get; set; }


    public UpdateTodoRequest()
    {
        IsComplete = false;
    }
}