namespace Harjoitustyo.Models
{
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

        public override string ToString()
        {
            return $"{this.Product.ProductName}\t\t\t{this.Quantity}\t\t\t{this.Product.Unit}\t\t\t{this.Product.Price}\t\t\t{this.Sum}";
        }
    }
}
