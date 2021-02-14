using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// Fetches top 10 categories based on user input from db
        /// </summary>
        public async Task<List<Category>> FetchAsync(List<RecipeCategory> recipeCategories, string filter)
        {
            return await db.Categories.Where(c => !recipeCategories.Select(rc => rc.Category).Contains(c) && c.Name.StartsWith(filter))
                .OrderByDescending(c => c.RecipeCategories.Count).Take(10).ToListAsync();
        }

        /// <summary>
        /// Checks if category already exists
        /// </summary>
        public async Task<Category> GetAsync(string name) => await db.Categories.FirstOrDefaultAsync(c => c.Name == name);
    }
}
