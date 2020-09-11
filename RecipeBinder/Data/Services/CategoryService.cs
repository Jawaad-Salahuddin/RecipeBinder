using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeBinder.Data.Models;

namespace RecipeBinder.Data.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext db;

        public CategoryService(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<List<Category>> FetchAsync(List<RecipeCategory> recipeCategories, string filter)
        {
            return await db.Categories.Where(c => !recipeCategories.Select(rc => rc.Category).Contains(c) && c.Name.StartsWith(filter)).Take(10).ToListAsync();
        }

        public async Task<Category> GetAsync(string name)
        {
            return await db.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
