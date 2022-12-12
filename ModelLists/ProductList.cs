using Harjoitustyo.Models;

namespace Harjoitustyo.ModelLists
{
    internal class ProductList
    {
        private List<Product> productList = new();

        public void AddToProductList(Product product)
        {
            this.productList.Add(product);
        }
    }
}
