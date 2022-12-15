using Harjoitustyo.Models;
using System.Text.Json;

namespace Harjoitustyo.Repos
{
    /// <summary>
    /// Luokka joka hoitaa laskukokoelman tiedostoon tallentamisen sekä tiedostosta lataamisen
    /// </summary>
    internal class InvoiceListRepo
    {
        /// <summary>
        /// Staattinen metodi joka serialisoi parametrinä saadun listan JSON muotoon ja tallentaa sen
        /// </summary>
        /// <param name="invoiceList">Invoice tyyppisistä olioista koostuva lista</param>
        public static void SaveJSON(List<Invoice> invoiceList)
        {
            string jsonString = JsonSerializer.Serialize(invoiceList);

            using (StreamWriter sw = File.CreateText("Invoices.json"))
            {
                sw.WriteLine(jsonString);
            }
        }

        /// <summary>
        /// Staattinen metodi joka lataa tallennetun JSON-tiedoston, deserialisoi sen ja palauttaa kutsujalle
        /// </summary>
        /// <returns>Invoice tyyppisistä olioista koostuva lista</returns>
        public static List<Invoice> LoadJSON()
        {
            if (File.Exists("Invoices.json"))
            {
                string? jsonString;

                using (StreamReader sr = File.OpenText("Invoices.json"))
                {
                    jsonString = sr.ReadLine();
                }

                return JsonSerializer.Deserialize<List<Invoice>>(jsonString);
            }
            else
            {
                return new List<Invoice>();
            }
        }
    }
}
