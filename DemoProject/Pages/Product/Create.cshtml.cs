using System.ComponentModel.DataAnnotations;
using DemoProject.Context;
using DemoProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using D = DemoProject.Models;

namespace DemoProject.Pages.Product
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;

        public CreateModel(CategoryService categoryService, ProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public List<D.Product> Products { get; set; }

        public List<SelectListItem> Categories { get; set; }

        [BindProperty]
        public ProductInputModel Product { get; set; }

        public async Task OnGetAsync()
        {
            await LoadData();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadData();

            if (!ModelState.IsValid)
                return Page();

            var dto = new ProductCreateDto
            {
                Name = Product.Name,
                Price = Product.Price,
                Description = Product.Description,
                Image = Product.Image,
                CategoryId = Product.CategoryId
            };

            await _productService.CreateProductAsync(dto);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToPage();
        }

        private async Task LoadData()
        {
            Products = await _productService.GetAllAsync();

            var categories = await _categoryService.GetAllAsync();

            Categories = categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();
        }
    }

    public class ProductInputModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public IFormFile? Image { get; set; }

        public Guid CategoryId { get; set; }
    }

}
