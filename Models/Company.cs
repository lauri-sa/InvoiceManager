namespace Harjoitustyo.Models
{
    /// <summary>
    /// Luokka joka toimii laskuttavan yrityksen tietomallina
    /// </summary>
    internal class Company
    {
        public string CompanyName { get; set; }
        public Address Address { get; set; }

        public Company(string companyName, Address address)
        {
            this.CompanyName = companyName;
            this.Address = address;
        }
    }
}