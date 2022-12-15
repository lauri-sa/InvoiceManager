using Harjoitustyo.Models;

namespace Harjoitustyo.ModelLists
{
    /// <summary>
    /// Luokka joka sisältää InvoiceLine tyyppisistä olioista koostuvan listan
    /// </summary>
    internal class InvoiceLineList
    {
        public List<InvoiceLine> InvoiceLines { get; set; }

        public InvoiceLineList()
        {
            this.InvoiceLines = new();
        }

        /// <summary>
        /// Lisää InvoiceLine tyyppisen olion listaan
        /// </summary>
        /// <param name="invoiceLine">InvoiceLine tyyppinen olio</param>
        public void AddToInvoiceLineList(InvoiceLine invoiceLine)
        {
            this.InvoiceLines.Add(invoiceLine);
        }

        /// <summary>
        /// Palauttaa listan joka koostuu InvoiceLine tyyppisistä olioista
        /// </summary>
        /// <returns>InvoiceLine olioista koostuvan listan</returns>
        public List<InvoiceLine> GetInvoiceLineList()
        {
            return this.InvoiceLines;
        }

        /// <summary>
        /// Laskee ja palauttaa kaikkien laskurivien yhteenlasketun summan
        /// </summary>
        /// <returns>Yhteenlasketun summan double tyyppisenä</returns>
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
