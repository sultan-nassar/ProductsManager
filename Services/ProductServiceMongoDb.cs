using MongoDB.Bson;
using MongoDB.Driver;
using ProductsManager.Interfaces;
using ProductsManager.Services;
using System.Xml.Linq;

namespace ProductsManager.Models
{
    public  class ProductServiceMongoDb: IProductService
    {

        private readonly IMongoCollection<Product> _Product;


        //this is not DI for the One way in controller becuase i have sent to constructor not to the whole environment
        //this make the service already depend on this constructor of the controller
        //but in another hand its DI for two way in Controller
        public ProductServiceMongoDb(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Product_Manager");
            _Product = database.GetCollection<Product>("Products");
            if (_Product.CountDocuments(FilterDefinition<Product>.Empty) == 0)
            {
                // Insert a default product
                InsertDefaultProduct();
            }
        }




        //List<products> getAll()

        public async Task<List<Product>> GetAll()
        {
            return await _Product.Find(_ => true).ToListAsync();
        }


        //products getone(int id)

        public async Task<Product> GetOne(string id)
        {
            Product product =await _Product.Find(x => x.Id == new ObjectId(id)).FirstOrDefaultAsync(); 
            if (product == null)
            {
                throw new Exception("there is no product with this ID " + id);
            }
            else
            {
                return product; 
            }
           
        }



        //public Product GetOne(int id)
        //{
        //    return DbService.Products.Find(p => p.Id == id);
        //}


        public async Task<Product> EditProduct(string id, Product updatedProduct)
        {
            var filter = Builders<Product>.Filter.Eq(u => u.Id, new ObjectId(id));

            // Include the update for BizNumber along with other properties
            var update = Builders<Product>.Update
                .Set(p => p.Name, updatedProduct.Name)
                .Set(c => c.Category, updatedProduct.Category)
                .Set(c => c.Description, updatedProduct.Description)
                .Set(c => c.Price, updatedProduct.Price)
                .Set(c => c.Stock, updatedProduct.Stock);


            // Check if BizNumber is not null before setting it


            var result = await _Product.UpdateOneAsync(filter, update);

            // Check if the update was successful
            if (result.MatchedCount == 0)
            {
                throw new Exception("Card not found");
            }

            return updatedProduct;
        }
            //void editProduct(int id, product p)


            //public void EditProduct(int id, Product updatedProduct)
            //{
            //    var specificProduct = DbService.Products.FirstOrDefault(p => p.Id == id);

            //    if (specificProduct != null)
            //    {
            //        // Update the properties of the existing product with the values from the updated product
            //        specificProduct.Name = updatedProduct.Name;
            //        specificProduct.Description = updatedProduct.Description;
            //        specificProduct.Price = updatedProduct.Price;
            //        specificProduct.Stock = updatedProduct.Stock;
            //        specificProduct.Category = updatedProduct.Category;
            //        // ... update other properties as needed
            //    }
            //    else
            //    {
            //        // Handle the case where the product with the specified id is not found
            //        // You might want to throw an exception, log an error, or take other appropriate actions.
            //    }
            //}



            //void deleteProduct(int id)

            public async Task DeleteProduct(string id)
        {
            var specificProductToDelete =await _Product.DeleteOneAsync(p => p.Id == new ObjectId(id));

            if (specificProductToDelete.DeletedCount==0)
            {
                throw new Exception("product not found");
            }
        }


        //void addNewProduct(product p)

        public async Task<Product> AddNewProduct(Product p)
        {
            p.Id = ObjectId.GenerateNewId();
            _Product.InsertOneAsync(p);
            return p;
        }


        private void InsertDefaultProduct()
        {
            var defaultProducts = new List<Product>
                {
                    new Product
                    {
                        Name = "Laptop",
                        Description = "High-performance gaming laptop",
                        Price = 1200.00m,
                        Stock = 10,
                        Category = "Electronics"
                    },
                    new Product
                    {
                        Name = "Smartphone",
                        Description = "Latest model with advanced features",
                        Price = 800.00m,
                        Stock = 20,
                        Category = "Phones"
                    },
                    new Product
                    {
                        Name = "Coffee Maker",
                        Description = "Automatic coffee maker with multiple settings",
                        Price = 150.00m,
                        Stock = 15,
                        Category = "Home Appliances"
                    },
            };

            _Product.InsertManyAsync(defaultProducts);
        }

    }
}
