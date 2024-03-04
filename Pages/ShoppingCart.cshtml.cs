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
        public async Task<IActionResult> OnPostCart(string id)
        {
            Cart = HttpContext.Session.Get<List<string>>("Cart") ?? new List<string>();

            // Check if the product id is already in the cart
            if (Cart.Contains(id))
                        {
                            // If it's in the cart, remove it
                            Cart.Remove(id);
                        }
                      

            // Update the cart in the session
            HttpContext.Session.Set("Cart", Cart);

            // Redirect to the same page or another page as needed
            return RedirectToPage("ShoppingCart");

        }

                  
    
    }
}
