using Harjoitustyo.Models;
using System.Text.Json;

namespace Harjoitustyo.Repos
{
    internal class ProductListRepo
    {
        private static List<Product> productList = new();

        public static void AddToProductList(Product product)
        {
            LoadProductList();

            product.ID = productList.Count + 1;

            productList.Add(product);

            SaveProductList();
        }

        public static List<Product> GetProductList()
        {
            LoadProductList();

            return productList;
        }

        private static void SaveProductList()
        {
            string jsonString = JsonSerializer.Serialize(productList);

            using (StreamWriter fs = File.CreateText("Products.json"))
            {
                fs.WriteLine(jsonString);

                fs.Close();
            }
        }

        private static void LoadProductList()
        {
            if (File.Exists("Products.json"))
            {
                string? jsonString;

                using (StreamReader sr = File.OpenText("Products.json"))
                {
                    jsonString = sr.ReadLine();

                    sr.Close();
                }

                productList = JsonSerializer.Deserialize<List<Product>>(jsonString);
            }
        }
    }
}