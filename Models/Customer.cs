namespace Harjoitustyo.Models
{
    internal class Customer
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public Customer(string name, Address address)
        {
            this.Name = name;
            this.Address = address;
        }

        public override string ToString()
        {
            return $"{this.Name}\n{this.Address}";
        }
    }
}
