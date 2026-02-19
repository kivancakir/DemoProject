using DemoProject.Context;
using DemoProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Services
{
    public class CategoryService
    {
        private readonly DemoProjectDbContext context;
        public CategoryService(DemoProjectDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(string name)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null) return;

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, string name)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null) return;

            category.Name = name;
            await context.SaveChangesAsync();
        }
    }
}
