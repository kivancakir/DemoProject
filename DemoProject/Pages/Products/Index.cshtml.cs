using DemoProject.Models;
using DemoProject.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using D = DemoProject.Models;

namespace DemoProject.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductService productService;
        public IndexModel(ProductService productService)
        {
            this.productService = productService;
        }

        public List<D.Product> Products { get; set; } = new();

        public async Task OnGetAsync()
        {
            Products = await productService.GetAllAsync();
        }
    }
}
