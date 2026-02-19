using DemoProject.Context;
using DemoProject.Models;
using DemoProject.Pages.Product;
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
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null) return;

            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        public async Task CreateProductAsync(ProductCreateDto dto)
        {
            // 🔹 Kategori var mı kontrol
            var categoryExists = await context.Categories
                .AnyAsync(c => c.Id == dto.CategoryId);

            if (!categoryExists)
                throw new Exception("Seçilen kategori bulunamadı.");

            string? imagePath = null;

            // 🔹 Resim yükleme
            if (dto.Image != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var extension = Path.GetExtension(dto.Image.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    throw new Exception("Geçersiz dosya formatı.");

                if (dto.Image.Length > 5 * 1024 * 1024)
                    throw new Exception("Dosya boyutu 5MB'dan büyük olamaz.");

                string imagesFolder = Path.Combine(env.WebRootPath, "images");

                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                string fileName = Guid.NewGuid() + extension;
                string filePath = Path.Combine(imagesFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                imagePath = "/images/" + fileName;
            }

            // 🔹 Product oluştur
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                ImagePath = imagePath,
                CategoryId = dto.CategoryId
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

    }
}
