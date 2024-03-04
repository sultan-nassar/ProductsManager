using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson.IO;
using ProductsManager.Interfaces;
using ProductsManager.Models;
using ProductsManager.Services;
using System.Text.Json;

namespace ProductsManager.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> Products { get; private set; }
        private IProductService _productsDbService;

        public IndexModel(IProductService productServiceMongoDb)
        {
            _productsDbService = productServiceMongoDb; //for the initiation of the object in the constructor
        }

        public async Task<IActionResult> OnGet()
        {
            Products = await _productsDbService.GetAll();
            return Page();

        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            // Implement the logic to delete the product with the specified id.
            _productsDbService.DeleteProduct(id);

            // Redirect to the home page after deletion.

            return RedirectToPage();
        }

       public async Task<IActionResult> OnPostShoppingCart(string id)
        {
            // Initialize cart
            var cart = HttpContext.Session.Get<List<string>>("Cart") ?? new List<string>();

            // Check if the product id is already in the cart
            if (cart.Contains(id))
            {
                // If it's in the cart, remove it
                cart.Remove(id);
            }
            else
            {
                // If it's not in the cart, add it
                cart.Add(id);
            }

            // Update the cart in the session
            HttpContext.Session.Set("Cart", cart);

            // Redirect to the same page or another page as needed
            return RedirectToPage("ShoppingCart");
        }

    }
    public static class SessionExtensions
    {
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
    }
}
