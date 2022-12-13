using Harjoitustyo.Models;
using Harjoitustyo.Repos;

namespace Harjoitustyo.ModelLists
{
    internal class InvoiceList
    {
        private static List<Invoice> invoiceList = new();

        public static void AddToInvoiceList(Invoice invoice)
        {
            invoiceList = InvoiceListRepo.LoadJSON();

            invoice.ID = invoiceList.Count + 1;

            invoiceList.Add(invoice);

            InvoiceListRepo.SaveJSON(invoiceList);
        }

        public static List<Invoice> GetInvoiceList()
        {
            invoiceList = InvoiceListRepo.LoadJSON();

            return invoiceList;
        }
    }
}
