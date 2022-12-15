using Harjoitustyo.Models;
using Harjoitustyo.Repos;

namespace Harjoitustyo.ModelLists
{
    /// <summary>
    /// Luokka joka sisältää Invoice tyyppisistä olioista koostuvan listan 
    /// </summary>
    internal class InvoiceList
    {
        private static List<Invoice> invoiceList = new();

        /// <summary>
        /// Pyytää listan JSON-tiedostosta, lisää parametrinä annetun Invoice tyyppisen olion ID ominaisuuden arvoksi listan pituus + 1,
        /// lisää Invoice tyyppisen olion listaan ja kutsuu metodia joka tallentaa listan tiedostoon
        /// </summary>
        /// <param name="invoice">Invoice tyyppinen olio</param>
        public static void AddToInvoiceList(Invoice invoice)
        {
            invoiceList = InvoiceListRepo.LoadJSON();

            invoice.ID = invoiceList.Count + 1;

            invoiceList.Add(invoice);

            InvoiceListRepo.SaveJSON(invoiceList);
        }

        /// <summary>
        /// Pyytää listan JSON-tiedostosta ja palauttaa sen kutsujalle
        /// </summary>
        /// <returns>Invoice tyyppisen listan</returns>
        public static List<Invoice> GetInvoiceList()
        {
            invoiceList = InvoiceListRepo.LoadJSON();

            return invoiceList;
        }
    }
}
