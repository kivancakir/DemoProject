using D = DemoProject.Models;
using DemoProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoProject.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly CategoryService _categoryService;

        public CreateModel(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public List<D.Category> Categories { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public Guid EditId { get; set; }

        [BindProperty]
        public string EditName { get; set; }

        public async Task OnGetAsync()
        {
            Categories = await _categoryService.GetAllAsync();
        }

        // EKLE
        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrWhiteSpace(Name))
                await _categoryService.CreateAsync(Name);

            return RedirectToPage();
        }

        // SİL
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToPage();
        }

        // GÜNCELLE
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!string.IsNullOrWhiteSpace(EditName))
                await _categoryService.UpdateAsync(EditId, EditName);

            return RedirectToPage();
        }
    }
}
