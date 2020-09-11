using System.ComponentModel.DataAnnotations;

namespace RecipeBinder.Data.Models
{
    public class Direction
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public int Step { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
