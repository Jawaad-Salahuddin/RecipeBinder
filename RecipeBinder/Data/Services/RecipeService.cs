using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Recipe>> FetchAsync(string search, string user, List<Category> categories)
        {
            IQueryable<Recipe> recipes = db.Recipes.Where(r => r.Name.Contains(search));
            if (categories.Count() > 0) recipes = recipes.Where(r => r.RecipeCategories.Any(rc => categories.Any(c => c.Equals(rc.Category))));
            if (user == null)
            {
                return await recipes.Where(r => !r.Restricted).ToListAsync();
            }
            else
            {
                return await recipes.Where(r => user == r.Owner || r.Users.Any(u => user == u.Email)).ToListAsync();
            }
        }

        public async Task<Recipe> GetAsync(int id)
        {
            return await db.Recipes.FindAsync(id);
        }

        public async Task<int> CreateAsync(Recipe recipe)
        {
            db.Recipes.Add(recipe);
            await db.SaveChangesAsync();
            return recipe.Id;
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            db.Entry(recipe).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Recipe recipe)
        {
            db.Recipes.Remove(recipe);
            await db.SaveChangesAsync();
        }
    }
}
