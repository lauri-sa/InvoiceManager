using Harjoitustyo.Models;
using Harjoitustyo.Repos;

namespace Harjoitustyo.ModelLists
{
    internal class ProductList
    {
        private static List<Product> productList = new();

        public static void AddToProductList(Product product)
        {
            productList = ProductListRepo.LoadJSON();

            product.ID = productList.Count + 1;

            productList.Add(product);

            ProductListRepo.SaveJSON(productList);
        }

        public static List<Product> GetProductList()
        {
            productList = ProductListRepo.LoadJSON();

            return productList;
        }

        public static int GetProductListLength()
        {
            return productList.Count;
        }
    }
}