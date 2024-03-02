using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ProductsManager.Models
{
     public class Product
    {
        public ObjectId Id { get; set; }
        //public string _id => Id.ToString();

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a price.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid positive price.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter a stock value.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid positive stock value.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public string Category { get; set; }
    }
}
