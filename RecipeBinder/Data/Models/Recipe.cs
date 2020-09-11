using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecipeBinder.Data.Attributes;

namespace RecipeBinder.Data.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Owner { get; set; }

        [Required]
        public string Name { get; set; }

        [ValidIngredients]
        public virtual List<Ingredient> Ingredients { get; set; }

        [ValidDirections]
        public virtual List<Direction> Directions { get; set; }

        public virtual List<RecipeCategory> RecipeCategories { get; set; }

        public string PrepTime { get; set; }

        public string CookTime { get; set; }

        public string Servings { get; set; }

        public bool Restricted { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
