using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsManager.Interfaces;
using ProductsManager.Models;

namespace ProductsManager.Pages
{
    public class AddProductModel : PageModel
	{

		[BindProperty] //i  use attribute to bind the Product property to the form fields.(asp-for) 
		public Product Product { get; set; }
		private IProductService _productsDbService;

		public AddProductModel(IProductService productServiceMongoDb)
        {
            _productsDbService = productServiceMongoDb;

        }

        public void OnGet()
		{
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page(); //return the page of this model(addproduct)
			}
			_productsDbService.AddNewProduct(Product);
			return RedirectToPage("Index");
		}
	}
}

