using Harjoitustyo.Models;
using System.Text.Json;

namespace Harjoitustyo.Repos
{
    internal class InvoiceListRepo
    {
        public static void SaveJSON(List<Invoice> invoiceList)
        {
            string jsonString = JsonSerializer.Serialize(invoiceList);

            using (StreamWriter sw = File.CreateText("Invoices.json"))
            {
                sw.WriteLine(jsonString);
            }
        }

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
