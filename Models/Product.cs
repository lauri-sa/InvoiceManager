namespace Harjoitustyo.Models
{
    /// <summary>
    /// Luokka joka toimii tuotteen tietomallina
    /// </summary>
    internal class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }

        public Product(string productName, string unit, double price)
        {
            this.ProductName = productName;
            this.Unit = unit;
            this.Price = price;
        }
    }
}
