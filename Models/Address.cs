namespace Harjoitustyo.Models
{
    /// <summary>
    /// Luokka joka toimii osoitetietojen tietomallina
    /// </summary>
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

        /// <summary>
        /// Ylikirjoitettu ToString metodi joka palauttaa luokan ominaisuuksien arvot string muodossa
        /// </summary>
        /// <returns>Luokan ominaisuuksien arvot string tyyppisenä</returns>
        public override string ToString()
        {
            return $"{this.StreetAddress}\n{this.PostalCode} {this.City}";
        }
    }
}
