namespace DemoProject.Pages.Product
{
    public class ProductCreateDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public IFormFile? Image { get; set; }

        public Guid CategoryId { get; set; }
    }
}
