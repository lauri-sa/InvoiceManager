using Harjoitustyo.Models;
using System.Text.Json;

namespace Harjoitustyo.Repos
{
    internal class InvoiceListRepo
    {
        private static List<Invoice> invoiceList = new();

        public static void AddToInvoiceList(Invoice invoice)
        {
            //LoadInvoiceList();

            invoice.ID = invoiceList.Count + 1;
            
            invoiceList.Add(invoice);

            //SaveInvoiceList();
        }

        public static List<Invoice> GetInvoiceList()
        {
            //LoadInvoiceList();

            return invoiceList;
        }

        private static void SaveInvoiceList()
        {
            string jsonString = JsonSerializer.Serialize(invoiceList);

            using (StreamWriter sw = File.CreateText("Invoices.json"))
            {
                sw.WriteLine(jsonString);

                sw.Close();
            }
        }

        private static void LoadInvoiceList()
        {
            if (File.Exists("Invoices.json"))
            {
                string? jsonString;

                using (StreamReader sr = File.OpenText("Invoices.json"))
                {
                    jsonString = sr.ReadLine();

                    sr.Close();
                }

                invoiceList = JsonSerializer.Deserialize<List<Invoice>>(jsonString);
            }
        }
    }
}
