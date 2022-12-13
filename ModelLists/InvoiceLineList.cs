using Harjoitustyo.Models;

namespace Harjoitustyo.ModelLists
{
    internal class InvoiceLineList
    {
        public List<InvoiceLine> InvoiceLines { get; set; }

        public InvoiceLineList()
        {
            this.InvoiceLines = new();
        }

        public void AddToInvoiceLineList(InvoiceLine invoiceLine)
        {
            this.InvoiceLines.Add(invoiceLine);
        }

        public List<InvoiceLine> GetInvoiceLineList()
        {
            return this.InvoiceLines;
        }

        public double GetSum()
        {
            double sum = 0;

            foreach(InvoiceLine invoiceLine in InvoiceLines)
            {
                sum += invoiceLine.Sum;
            }

            return sum;
        }
    }
}
