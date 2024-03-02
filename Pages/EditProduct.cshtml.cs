using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsManager.Interfaces;
using ProductsManager.Models;

namespace ProductsManager.Pages
{
    public class EditProductModel : PageModel
    {
        private readonly IProductService _productService; // Inject your product service here

        [BindProperty]
        public Product Product { get; set; }

        public EditProductModel(IProductService productServiceMongoDb)
        {
            _productService = productServiceMongoDb;

        }

        public async Task<IActionResult> OnGet(string id)
        {
            // Fetch the product from your data source based on the ID
            Product = await _productService.GetOne(id);

            if (Product == null)
            {
                return NotFound(); // Product not found
            }

            return Page(); //return the page of EditProduct of this model
        }

        // Implement the OnPost method to handle the form submission
        public IActionResult OnPost(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Perform the update logic using productService.UpdateProduct(Product)
             _productService.EditProduct(id, Product);

            return RedirectToPage("/Index");
        }
    }
}
