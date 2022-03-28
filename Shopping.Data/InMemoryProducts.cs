using Shopping.Core;
using Shopping.Core.Domain;
using Shopping.Data.Interfaces;

namespace Shopping.Data
{
    public class InMemoryProducts : IProducts
    {
        private readonly List<Product> products;
        public InMemoryProducts()
        {
            products = new List<Product>()
            {
              new Product{ ProductId=1,ProductName=ProductName.Milk,Price=1.00m},
              new Product{ ProductId=2,ProductName=ProductName.Butter,Price=2.00m},
              new Product{ ProductId=3,ProductName=ProductName.Sugar,Price=2.50m},
              new Product{ ProductId=4,ProductName=ProductName.Egg,Price=1.50m}
            };
        }

        public List<Product> GetavailableProducts()
        {
            return products.ToList();
        }
    }
}
