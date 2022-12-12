using Harjoitustyo.Models;

namespace Harjoitustyo.ModelLists
{
    internal class InvoiceList
    {
        private List<Invoice> invoiceList = new();

        public void AddToInvoiceList(Invoice invoice)
        {
            invoice.ID = invoiceList.Count + 1;
            this.invoiceList.Add(invoice);
        }
    }
}
