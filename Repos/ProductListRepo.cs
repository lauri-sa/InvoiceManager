using Harjoitustyo.Models;
using System.Text.Json;

namespace Harjoitustyo.Repos
{
    internal class ProductListRepo
    {
        

        public static void SaveJSON(List<Product> productList)
        {
            string jsonString = JsonSerializer.Serialize(productList);

            using (StreamWriter sw = File.CreateText("Products.json"))
            {
                sw.WriteLine(jsonString);
            }
        }

        public static List<Product> LoadJSON()
        {
            if (File.Exists("Products.json"))
            {
                string? jsonString;

                using (StreamReader sr = File.OpenText("Products.json"))
                {
                    jsonString = sr.ReadLine();
                }

                return JsonSerializer.Deserialize<List<Product>>(jsonString);
            }
            else
            {
                return new List<Product>();
            }
        }
    }
}