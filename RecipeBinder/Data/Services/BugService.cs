using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeBinder.Data.Models;

namespace RecipeBinder.Data.Services
{
    public class BugService
    {
        private readonly ApplicationDbContext db;

        public BugService(ApplicationDbContext context)
        {
            db = context;
        }

        /// <summary>
        /// Fetches bugs from db
        /// </summary>
        public async Task<List<Bug>> FetchAsync() => await db.Bugs.ToListAsync();

        /// <summary>
        /// Adds bug to db
        /// </summary>
        public async Task CreateAsync(Bug bug)
        {
            db.Bugs.Add(bug);
            await db.SaveChangesAsync();
        }
    }
}
