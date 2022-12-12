using Harjoitustyo.Models;

namespace Harjoitustyo.ModelLists
{
    internal class InvoiceLineList
    {
        private List<InvoiceLine> invoiceLineList = new();

        public void AddToInvoiceLineList(InvoiceLine invoiceLine)
        {
            this.invoiceLineList.Add(invoiceLine);
        }

        public double GetSum()
        {
            double sum = 0;

            foreach(InvoiceLine invoiceLine in invoiceLineList)
            {
                sum += invoiceLine.Sum;
            }

            return sum;
        }
    }
}
