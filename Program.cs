using Harjoitustyo.ModelLists;
using Harjoitustyo.Models;

namespace Harjoitustyo
{
    internal class Program
    {
        /// <summary>
        /// Tulostaa parametrinä annetun merkkijonon punaisella konsoliin
        /// </summary>
        /// <param name="message">Merkkijono</param>
        static void ErrorMessage(string message)
        {
            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);

            Console.ForegroundColor = color;
        }

        /// <summary>
        /// Siirtää kursorin konsolin vasempaan yläkulmaan ja jää odottamaan enter napin painallusta käyttäjältä
        /// </summary>
        static void ReturnToMainMenu()
        {
            Console.Write("Paina enter palataksesi päävalikkoon");

            Console.SetCursorPosition(0, 0);

            while (true)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Kutsuu erillisiä metodeita jotka palauttavat käyttäjän antamat arvot muuttujiin ja palauttaa niiden avulla luodun Address olion
        /// </summary>
        /// <param name="info"></param>
        /// <returns>Address tyyppinen olio</returns>
        static Address CreateAddress(string info)
        {
            var streetAddress = ValidateString($"Anna {info} katuosoite: ");

            var postalCode = ValidateString($"Anna {info} postinumero: ");

            var city = ValidateString($"Anna {info} asuinpaikkakunta: ");

            return new Address(streetAddress, postalCode, city);
        }

        /// <summary>
        /// Kutsuu erillisiä metodeita jotka palauttavat käyttäjän antamat arvot muuttujiin ja palauttaa niiden avulla luodun Customer olion
        /// </summary>
        /// <returns>Customer tyyppinen olio</returns>
        static Customer CreateCustomer()
        {
            var name = ValidateString("Anna asiakkaan nimi: ");

            var address = CreateAddress("asiakkaan");

            return new Customer(name, address);
        }

        /// <summary>
        /// Kutsuu erillisiä metodeita jotka palauttavat käyttäjän antamat arvot muuttujiin ja palauttaa niiden avulla luodun Company olion
        /// </summary>
        /// <returns>Company tyyppinen olio</returns>
        static Company CreateCompany()
        {
            var name = ValidateString("Anna laskuttajan nimi: ");

            var address = CreateAddress("laskuttajan");

            return new Company(name, address);
        }

        /// <summary>
        /// Kysyy käyttäjältä kokonaisluvun, lisää sen nykyiseen päivämäärään AddDays metodilla ja palauttaa tämän lopputuloksen merkkijonona
        /// </summary>
        /// <returns>Päivämäärä merkkijonona</returns>
        static string CreateExpirationDate()
        {
            int days;

            while (true)
            {
                Console.Write("Anna maksuaika päivinä (minimi 14 päivää): ");

                if (!int.TryParse(Console.ReadLine(), out days) || days < 14)
                {
                    Console.Clear();
                    ErrorMessage("Syöte on väärin\n");
                }
                else
                {
                    return DateTime.Now.AddDays(days).ToString("dd.MM.yyyy");
                }
            }
        }

        /// <summary>
        /// Luo laskurivikokoelman, pyytää käyttäjältä tiedot laskuriviin ja lisää laskurivin kokoelmaan. Palauttaa laskurivikokoelman.
        /// </summary>
        /// <returns>InvoiceLineList tyyppisen olion</returns>
        static InvoiceLineList CreateInvoiceLineList()
        {
            bool loop = true;

            var invoiceLineList = new InvoiceLineList();

            while (loop)
            {
                var product = ChooseProduct();

                var quantity = ValidateInt("Anna tuotteen kappalemäärä: ");

                invoiceLineList.AddToInvoiceLineList(new InvoiceLine(product, quantity));

                while (true)
                {
                    Console.Clear();

                    Console.WriteLine("Tuoterivi lisätty laskulle onnistuneesti. Haluatko lisätä uuden tuoterivin? (k/e)");

                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.K)
                    {
                        break;
                    }
                    else if (key == ConsoleKey.E)
                    {
                        loop = false;

                        Console.Clear();

                        break;
                    }
                }
            }

            return invoiceLineList;
        }

        /// <summary>
        /// Tulostaa tuotelistan, kysyy käyttäjältä tuotteen numeroa ja palauttaa tuotteen listasta käyttäjän syötteen perusteella
        /// </summary>
        /// <returns>Product tyyppisen olion</returns>
        static Product ChooseProduct()
        {
            Console.Clear();

            var productList = ProductList.GetProductList();

            int productID;

            while (true)
            {
                productList.ForEach(product =>
                {
                    Console.WriteLine($"{product.ID}. {product.ProductName}\n");
                });

                Console.Write("Anna tuotteen numero: ");

                var userInput = Console.ReadLine();

                if (!int.TryParse(userInput, out productID) || productID < 1 || productID > productList.Count)
                {
                    Console.Clear();

                    ErrorMessage("Syöte on väärin\n");
                }
                else
                {
                    return productList[productID - 1];
                }
            }
        }

        /// <summary>
        /// Luo laskun ja lisää sen laskulistaan
        /// </summary>
        static void AddInvoice()
        {
            Console.Clear();

            if (ProductList.GetProductListLength() > 0)
            {
                var company = CreateCompany();

                var customer = CreateCustomer();

                var invoiceLineList = CreateInvoiceLineList();

                var expirationDate = CreateExpirationDate();

                string additionalInformation = string.Empty;

                while (true)
                {
                    Console.Clear();

                    Console.WriteLine("Haluatko antaa laskulle lisätietoja? (k/e)");

                    var key = Console.ReadKey().Key;

                    if (key == ConsoleKey.K)
                    {
                        additionalInformation = ValidateString("Anna laskun lisätieto: ");
                        InvoiceList.AddToInvoiceList(new Invoice(company, customer, invoiceLineList, expirationDate, additionalInformation));
                        break;
                    }
                    else if (key == ConsoleKey.E)
                    {
                        InvoiceList.AddToInvoiceList(new Invoice(company, customer, invoiceLineList, expirationDate, additionalInformation));
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Tuotetietokanta on tyhjä. Lisää tuotetietokantaan tuotteita ennen kuin voit jatkaa laskun luontiin\n");
                ReturnToMainMenu();
            }
        }

        /// <summary>
        /// Tulostaa konsoliin parametrinä saadun Invoice olion tiedot
        /// </summary>
        /// <param name="invoice">Invoice tyyppinen olio</param>
        static void PrintInvoice(Invoice invoice)
        {
            Console.WriteLine("LASKU\n");

            Console.WriteLine("Laskuttaja\n{0,-50}{1,-50}", invoice.Company.CompanyName, $"Laskun numero: {invoice.ID}");

            Console.WriteLine("{0,-50}{1,-50}", invoice.Company.Address.StreetAddress, $"Päiväys: {invoice.Date}");

            Console.WriteLine("{0,-50}{1,-50}", $"{invoice.Company.Address.PostalCode} {invoice.Company.Address.City}", $"Eräpäivä: {invoice.ExpirationDate}");

            Console.WriteLine($"\nAsiakas\n{invoice.Customer}\n");

            Console.WriteLine($"Lisätiedot: {invoice.AdditionalInformation}\n");

            Console.WriteLine("{0,-25}{1,-25}{2,-25}{3,-25}{4,-25}", "Tuote", "Määrä", "Yksikkö", "A-hinta", "Yhteensä");

            invoice.InvoiceLineList.GetInvoiceLineList().ForEach(invoiceLine => Console.WriteLine("{0,-25}{1,-25}{2,-25}{3,-25}{4,-25}", invoiceLine.Product.ProductName, invoiceLine.Quantity, invoiceLine.Product.Unit, invoiceLine.Product.Price, invoiceLine.Sum));

            Console.WriteLine("\n{0,-25}{1,-25}{2,-25}{3,-25}{4,-25}", "", "", "", "", $"YHTEENSÄ: {invoice.Sum} €");

            Console.WriteLine($"\n{new String('*', Console.WindowWidth)}\n");
        }

        /// <summary>
        /// Tulostaa kaikki laskulistassa olevat laskut konsoliin
        /// </summary>
        static void PrintAllInvoices()
        {
            Console.Clear();

            var invoiceList = InvoiceList.GetInvoiceList();

            if (invoiceList.Count > 0)
            {
                invoiceList.ForEach(invoice =>
                {
                    PrintInvoice(invoice);
                });
            }
            else
            {
                Console.WriteLine("Laskutietokanta on tyhjä\n");
            }
        }

        /// <summary>
        /// Kysyy käyttäjältä kokonaisluvun ja tulostaa sen perusteella löytyvän laskun laskulistasta
        /// </summary>
        static void PrintInvoiceBasedOnNumber()
        {
            Console.Clear();

            bool loop = true;

            var invoiceList = InvoiceList.GetInvoiceList();

            if (invoiceList.Count > 0)
            {
                while (loop)
                {
                    var id = ValidateInt("Anna laskun numero: ");

                    Console.Clear();

                    if (id < 1 || id > invoiceList.Count)
                    {
                        while (true)
                        {
                            Console.Clear();

                            Console.WriteLine("Tällä numerolla ei löydy laskua\n");

                            Console.WriteLine("Haluatko kokeilla uudestaan? (k/e)");

                            var key = Console.ReadKey(true).Key;

                            if (key == ConsoleKey.K)
                            {
                                break;
                            }
                            else if (key == ConsoleKey.E)
                            {
                                loop = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        PrintInvoice(invoiceList[id - 1]);

                        ReturnToMainMenu();

                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Laskutietokanta on tyhjä\n");

                ReturnToMainMenu();
            }
        }

        /// <summary>
        /// Kysyy käyttäjältä syötteenä nimen ja tulostaa sen perusteella löytyvän laskun/laskut laskulistasta
        /// </summary>
        static void PrintInvoiceBasedOnCustomer()
        {
            Console.Clear();

            bool loop = true;

            var invoiceList = InvoiceList.GetInvoiceList();

            if (invoiceList.Count > 0)
            {
                while (loop)
                {
                    var customerName = ValidateString("Anna asiakkaan nimi: ");

                    Console.Clear();

                    if (invoiceList.Any(invoice => invoice.Customer.Name == customerName))
                    {
                        invoiceList.ForEach(invoice =>
                        {
                            if (invoice.Customer.Name == customerName)
                            {
                                PrintInvoice(invoice);
                            }
                        });

                        ReturnToMainMenu();

                        break;
                    }
                    else
                    {
                        while (true)
                        {
                            Console.Clear();

                            Console.WriteLine("Tällä nimellä ei löydy laskua\n");

                            Console.WriteLine("Haluatko kokeilla uudestaan? (k/e)");

                            var key = Console.ReadKey(true).Key;

                            if (key == ConsoleKey.K)
                            {
                                break;
                            }
                            else if (key == ConsoleKey.E)
                            {
                                loop = false;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Laskutietokanta on tyhjä\n");

                ReturnToMainMenu();
            }
        }

        /// <summary>
        /// Tulostaa tuotelistan, kysyy käyttäjältä tuotteen numeroa ja tulostaa kaikki laskut laskulistasta jotka sisältävät kyseisen tuotteen
        /// </summary>
        static void PrintInvoicesBasedOnProduct()
        {
            Console.Clear();

            bool loop = true;

            var productList = ProductList.GetProductList();

            var invoiceList = InvoiceList.GetInvoiceList();

            if (invoiceList.Count > 0)
            {
                int productID;

                while (loop)
                {
                    productList.ForEach(product =>
                    {
                        Console.WriteLine($"{product.ID}. {product.ProductName}\n");
                    });

                    Console.Write("Anna tuotteen numero: ");

                    var userInput = Console.ReadLine();

                    Console.Clear();

                    if (!int.TryParse(userInput, out productID) || productID < 1 || productID > productList.Count)
                    {
                        ErrorMessage("Syöte on väärin\n");
                    }
                    else
                    {
                        bool found = false;

                        invoiceList.ForEach(invoice =>
                        {
                            var invoiceLineList = invoice.InvoiceLineList.GetInvoiceLineList();

                            if (invoiceLineList.Any(invoiceLine => invoiceLine.Product.ID == productID))
                            {
                                found = true;
                                PrintInvoice(invoice);
                            }
                        });

                        if (found)
                        {
                            ReturnToMainMenu();

                            break;
                        }
                        else
                        {
                            while (true)
                            {
                                Console.Clear();

                                Console.WriteLine("Tällä tuotteella ei löytynyt laskuja\n");

                                Console.WriteLine("Haluatko kokeilla uudestaan? (k/e)");

                                var key = Console.ReadKey().Key;

                                if (key == ConsoleKey.K)
                                {
                                    Console.Clear();
                                    break;
                                }
                                else if (key == ConsoleKey.E)
                                {
                                    loop = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Laskutietokanta on tyhjä\n");

                ReturnToMainMenu();
            }
        }

        /// <summary>
        /// Tulostaa tuotelistan konsoliin, kysyy haluaako käyttäjä lisätä tuotteen listaan ja lisää tuotteen jos käyttäjä haluaa
        /// </summary>
        static void AddProduct()
        {
            while (true)
            {
                PrintAllProducts();

                Console.WriteLine("Haluatko lisätä uuden tuotteen? (k/e)");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.K)
                {
                    ProductList.AddToProductList(CreateNewProduct());
                }
                else if (key == ConsoleKey.E)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Tulostaa kaikki tuotelistan tuotteet konsoliin
        /// </summary>
        static void PrintAllProducts()
        {
            Console.Clear();

            var separator = $"\n\n{new String('*', 50)}\n";

            var productList = ProductList.GetProductList();

            if (productList.Count > 0)
            {
                Console.WriteLine($"Tuotetietokannassa olevat tuotteet{separator}");

                productList.ForEach(product => { Console.WriteLine("{0,-25}{1,-25}{2}", $"Tuote: {product.ProductName}", $"Hinta: {product.Price} € / {product.Unit}", separator); });
            }
            else
            {
                Console.WriteLine("Tuotetietokanta on tyhjä\n");
            }
        }

        /// <summary>
        /// Kutsuu erillisiä metodeita jotka palauttavat käyttäjän antamat arvot muuttujiin ja palauttaa niiden avulla luodun Product olion
        /// </summary>
        /// <returns>Product tyyppinen olio</returns>
        static Product CreateNewProduct()
        {
            var productName = ValidateString("Anna tuotteen nimi: ");

            var unit = ValidateString("Anna tuotteen yksikkö: ");

            var price = ValidateDouble("Anna tuotteen hinta: ");

            return new Product(productName, unit, price);
        }

        /// <summary>
        /// Kysyy käyttäjältä string tyyppistä syötettä, tarkistaa että se ei ole tyhjä ja palauttaa hyväksytyn syötteen kutsujalle.
        /// Saa parametrinä infotekstin joka käyttäjälle näytetään
        /// </summary>
        /// <param name="info">string</param>
        /// <returns>string</returns>
        static string ValidateString(string info)
        {
            Console.Clear();

            string? userInput;

            while (true)
            {
                Console.Write($"{info}");

                userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.Clear();

                    ErrorMessage("Syöte ei voi olla tyhjä\n");
                }
                else
                {
                    break;
                }
            }

            return userInput;
        }

        /// <summary>
        /// Kysyy käyttäjältä int tyyppistä syötettä ja palauttaa hyväksytyn syötteen kutsujalle.
        /// Saa parametrinä infotekstin joka käyttäjälle näytetään
        /// </summary>
        /// <param name="info">string</param>
        /// <returns>int</returns>
        static int ValidateInt(string info)
        {
            Console.Clear();

            int userInput;

            while (true)
            {
                Console.Write($"{info}");

                if (!int.TryParse(Console.ReadLine(), out userInput))
                {
                    Console.Clear();

                    ErrorMessage("Syöte ei voi olla tyhjä ja sen täytyy olla numeerinen\n");
                }
                else
                {
                    break;
                }
            }

            return userInput;
        }

        /// <summary>
        /// Kysyy käyttäjältä double tyyppistä syötettä ja palauttaa hyväksytyn syötteen kutsujalle.
        /// Saa parametrinä infotekstin joka käyttäjälle näytetään
        /// </summary>
        /// <param name="info">string</param>
        /// <returns>double</returns>
        static double ValidateDouble(string info)
        {
            Console.Clear();

            double userInput;

            while (true)
            {
                Console.Write($"{info}");

                if (!double.TryParse(Console.ReadLine(), out userInput) || userInput <= 0)
                {
                    Console.Clear();

                    ErrorMessage("Syöte ei voi olla tyhjä, sen täytyy olla numeerinen sekä yli 0\n");
                }
                else
                {
                    break;
                }
            }

            return userInput;
        }

        static void Main(string[] args)
        {
            Console.Title = "Laskutussovellus";

            if (OperatingSystem.IsWindows())
            {
                Console.WindowWidth = 125;
            }

            Console.CursorVisible = false;

            Console.BackgroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.Black;

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Laskutussovellus\n");

                Console.WriteLine("1 = Lisää lasku\n");

                Console.WriteLine("2 = Lisää tuote\n");

                Console.WriteLine("3 = Tulosta kaikki laskut\n");

                Console.WriteLine("4 = Tulosta kaikki tuotteet\n");

                Console.WriteLine("5 = Tulosta lasku numeron perusteella\n");

                Console.WriteLine("6 = Tulosta lasku asiakkaan nimen perusteella\n");

                Console.WriteLine("7 = Tulosta laskut tuotteen perusteella\n");

                Console.Write("Esc = Sulje ohjelma");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.D1)
                {
                    AddInvoice();
                }
                else if (key == ConsoleKey.D2)
                {
                    AddProduct();
                }
                else if (key == ConsoleKey.D3)
                {
                    PrintAllInvoices();

                    ReturnToMainMenu();
                }
                else if (key == ConsoleKey.D4)
                {
                    PrintAllProducts();

                    ReturnToMainMenu();
                }
                else if (key == ConsoleKey.D5)
                {
                    PrintInvoiceBasedOnNumber();
                }
                else if (key == ConsoleKey.D6)
                {
                    PrintInvoiceBasedOnCustomer();
                }
                else if (key == ConsoleKey.D7)
                {
                    PrintInvoicesBasedOnProduct();
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}