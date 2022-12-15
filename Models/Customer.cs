namespace Harjoitustyo.Models
{
    /// <summary>
    /// Luokka joka toimii asiakkaan tietomallina
    /// </summary>
    internal class Customer
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public Customer(string name, Address address)
        {
            this.Name = name;
            this.Address = address;
        }

        /// <summary>
        /// Ylikirjoitettu ToString metodi joka palauttaa luokan ominaisuuksien arvot string muodossa
        /// </summary>
        /// <returns>Luokan ominaisuuksien arvot string tyyppisenä</returns>
        public override string ToString()
        {
            return $"{this.Name}\n{this.Address}";
        }
    }
}
