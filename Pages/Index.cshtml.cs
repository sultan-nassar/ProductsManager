using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsManager.Interfaces;
using ProductsManager.Models;
using ProductsManager.Services;

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
    } 
}
