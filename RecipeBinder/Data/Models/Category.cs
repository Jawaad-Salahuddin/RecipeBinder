using System.Collections.Generic;

namespace RecipeBinder.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<RecipeCategory> RecipeCategories { get; set; }
    }
}
