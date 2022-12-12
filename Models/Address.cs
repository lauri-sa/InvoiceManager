namespace Harjoitustyo.Models
{
    internal class Address
    {
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public Address(string streetAddress, string postalCode, string city)
        {
            this.StreetAddress = streetAddress;
            this.PostalCode = postalCode;
            this.City = city.ToUpper();
        }

        public override string ToString()
        {
            return $"{this.StreetAddress}\n{this.PostalCode} {this.City}";
        }
    }
}
