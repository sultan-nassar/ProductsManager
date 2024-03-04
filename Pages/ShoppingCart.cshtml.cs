using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsManager.Models;

namespace ProductsManager.Pages
{
        public class ShoppingCartModel : PageModel
    {
        public List<string> Cart { get; set; }

        public void OnGet()
        {
            // Retrieve cart from session
            Cart =  HttpContext.Session.Get<List<string>>("Cart") ?? new List<string>();

        }
    }
}
