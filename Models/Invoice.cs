using Harjoitustyo.ModelLists;
using System.Text.Json.Serialization;

namespace Harjoitustyo.Models
{
    internal class Invoice
    {
        public int ID { get; set; }
        public double Sum { get; }
        public string Date { get; }
        public string ExpirationDate { get; set; }
        public string? AdditionalInformation { get; set; }
        public Company Company { get; set; }
        public Customer Customer { get; set; }
        public InvoiceLineList InvoiceLineList { get; set; }

        public Invoice(Company company, Customer customer, InvoiceLineList invoiceLineList, string expirationDate, string additionalInformation)
        {
            this.Date = DateTime.Now.ToString("dd.MM.yyyy");
            this.Company = company;
            this.Customer = customer;
            this.InvoiceLineList = invoiceLineList;
            this.ExpirationDate = expirationDate;
            this.AdditionalInformation = additionalInformation;
            this.Sum = this.InvoiceLineList.GetSum();
        }
    }
}