namespace Harjoitustyo.Models
{
    /// <summary>
    /// Luokka joka toimii laskurivin tietomallina
    /// </summary>
    internal class InvoiceLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Sum { get; set; }

        public InvoiceLine(Product product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
            this.Sum = this.Quantity * this.Product.Price;
        }
    }
}
