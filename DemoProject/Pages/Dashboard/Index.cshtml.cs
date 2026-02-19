using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoProject.Pages.Dashboard
{
    [Authorize] // Login olmadan girilemez
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
