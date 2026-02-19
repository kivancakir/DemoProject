using DemoProject.Context;
using DemoProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Services
{
    public class ProductService
    {
        private readonly DemoProjectDbContext context;
        private readonly IWebHostEnvironment env;
        public ProductService(DemoProjectDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await context.Products
                           .OrderByDescending(x => x.CreatedAt)
                           .ToListAsync();
        }

        public async Task AddAsync(Product product, IFormFile? imageFile)
        {
            if (imageFile != null)
            {
                var folder = Path.Combine(env.WebRootPath, "images/products");
                Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                product.ImagePath = "/images/products/" + fileName;
            }

            context.Products.Add(product);
            await context.SaveChangesAsync();
        }
    }
}
