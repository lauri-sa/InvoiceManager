using Harjoitustyo.Models;
using Harjoitustyo.Repos;

namespace Harjoitustyo.ModelLists
{
    /// <summary>
    /// Luokka joka sisältää Product tyyppisistä olioista koostuvan listan
    /// </summary>
    internal class ProductList
    {
        private static List<Product> productList = new();

        /// <summary>
        /// Pyytää listan JSON-tiedostosta, lisää parametrinä annetun Product tyyppisen olion ID ominaisuuden arvoksi listan pituus + 1,
        /// lisää Product tyyppisen olion listaan ja kutsuu metodia joka tallentaa listan tiedostoon
        /// </summary>
        /// <param name="product"></param>
        public static void AddToProductList(Product product)
        {
            productList = ProductListRepo.LoadJSON();

            product.ID = productList.Count + 1;

            productList.Add(product);

            ProductListRepo.SaveJSON(productList);
        }

        /// <summary>
        /// Pyytää listan JSON-tiedostosta ja palauttaa sen kutsujalle
        /// </summary>
        /// <returns>Product tyyppisen listan</returns>
        public static List<Product> GetProductList()
        {
            productList = ProductListRepo.LoadJSON();

            return productList;
        }

        /// <summary>
        ///  Pyytää listan JSON-tiedostosta ja palauttaa kutsujalle sen pituuden
        /// </summary>
        /// <returns>Listan pituuden</returns>
        public static int GetProductListLength()
        {
            productList = GetProductList();

            return productList.Count;
        }
    }
}