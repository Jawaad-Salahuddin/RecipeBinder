using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeBinder.Data.Models;

namespace RecipeBinder.Data.Services
{
    public class RecipeService
    {
        private readonly ApplicationDbContext db;

        public RecipeService(ApplicationDbContext context)
        {
            db = context;
        }

        /// <summary>
        /// Fetches recipes based on user input from db
        /// </summary>
        public async Task<List<Recipe>> FetchAsync(string search, string user, List<Category> categories, Sort sort, Filter? filter)
        {
            IQueryable<Recipe> recipes = db.Recipes.Where(r => r.Name.Contains(search));
            if (categories.Count() > 0) recipes = recipes.Where(r => r.RecipeCategories.Any(rc => categories.Any(c => c.Equals(rc.Category))));
            if (user == null)
            {
                recipes = recipes.Where(r => !r.Restricted);
            }
            else
            {
                switch (filter)
                {
                    case Filter.Created:
                        recipes = recipes.Where(r => user == r.Owner);
                        break;
                    case Filter.Shared:
                        recipes = recipes.Where(r => r.Readers.Any(u => user == u.Email));
                        break;
                    case Filter.Liked:
                        recipes = recipes.Where(r => r.Likes.Any(l => user == l.Email));
                        break;
                }
            }
            switch (sort)
            {
                case Sort.Popularity:
                    recipes = recipes.OrderByDescending(r => r.Likes.Count).ThenByDescending(r => r.Id);
                    break;
                case Sort.Newest:
                    recipes = recipes.OrderByDescending(r => r.Id).ThenByDescending(r => r.Likes.Count);
                    break;
            }
            return await recipes.ToListAsync();
        }

        /// <summary>
        /// Gets recipe from db given id
        /// </summary>
        public async Task<Recipe> GetAsync(int id) => await db.Recipes.FindAsync(id);

        /// <summary>
        /// Adds recipe to db
        /// </summary>
        public async Task CreateAsync(Recipe recipe)
        {
            db.Recipes.Add(recipe);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Modifies db entry of recipe
        /// </summary>
        public async Task UpdateAsync(Recipe recipe)
        {
            db.Entry(recipe).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes recipe from db
        /// </summary>
        public async Task DeleteAsync(Recipe recipe)
        {
            foreach (RecipeCategory recipeCategory in recipe.RecipeCategories)
            {
                Category category = recipeCategory.Category;
                if (category.RecipeCategories.Count == 1) db.Categories.Remove(category);
            }
            db.Recipes.Remove(recipe);
            await db.SaveChangesAsync();
        }
    }
}
