using Harjoitustyo.Models;
using System.Text.Json;

namespace Harjoitustyo.Repos
{
    /// <summary>
    /// Luokka joka hoitaa tuotekokoelman tiedostoon tallentamisen sekä tiedostosta lataamisen
    /// </summary>
    internal class ProductListRepo
    {
        /// <summary>
        /// Staattinen metodi joka serialisoi parametrinä saadun listan JSON muotoon ja tallentaa sen
        /// </summary>
        /// <param name="productList">Product tyyppisistä olioista koostuva lista</param>
        public static void SaveJSON(List<Product> productList)
        {
            string jsonString = JsonSerializer.Serialize(productList);

            using (StreamWriter sw = File.CreateText("Products.json"))
            {
                sw.WriteLine(jsonString);
            }
        }

        /// <summary>
        /// Staattinen metodi joka lataa tallennetun JSON-tiedoston, deserialisoi sen ja palauttaa kutsujalle
        /// </summary>
        /// <returns>Product tyyppisistä olioista koostuva lista</returns>
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