
using System.ComponentModel.DataAnnotations;

namespace LearnDotnet.Models
{
    public class Todo
    {
        [Key]
        public Guid Id { get; set; }
        
        public String Title { get; set; }

        public String Description { get; set; }

        public bool IsComplete { get; set; }

        public DateTime DueDate { get; set; }

        public int Priority { get; set; }

        public DateTime CreateAt { get; set; } 
        
        public DateTime UpdatedAt { get; set; }

        public Todo () 
        {
            IsComplete = false;
        }

    }

}
