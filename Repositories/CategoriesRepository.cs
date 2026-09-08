using Microsoft.EntityFrameworkCore;
using Naringskollen.Data;
using Naringskollen.Models;
using Naringskollen.Repositories.IRepositories;

namespace Naringskollen.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly NaringskollenDbContext context;

        public CategoriesRepository(NaringskollenDbContext _context)
        {
            context = _context;
        }

        public async Task<List<Category>> GetAll()
        {
            var categories = await context.Categories
                .AsNoTracking()
                .ToListAsync();

            return categories;
        }

        public async Task<Category?> GetById(int id)
        {
            var category = await context.Categories
                .FindAsync(id);

            return category;
        }
    }
}
