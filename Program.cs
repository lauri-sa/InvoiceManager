using Harjoitustyo.Models;
using Harjoitustyo.Repos;

namespace Harjoitustyo
{
    internal class Program
    {
        static void ErrorMessage(string message)
        {
            var color = Console.ForegroundColor;
            
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);

            Console.ForegroundColor = color;
        }

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

        static void PrintInvoice(Invoice invoice)
        {
            Console.WriteLine("LASKU\n");
            
            Console.WriteLine($"Laskuttaja\n{invoice.Company.CompanyName}\t\t\tPäiväys: {invoice.Date}");
            
            Console.WriteLine($"{invoice.Company.Address.StreetAddress}\t\t\tLaskun numero: {invoice.ID}");
            
            Console.WriteLine($"{invoice.Company.Address.PostalCode} {invoice.Company.Address.City}\t\t\tEräpäivä: {invoice.ExpirationDate}\n");
            
            Console.WriteLine($"Asiakas\n{invoice.Customer}");
            
            Console.WriteLine($"Lisätiedot: {invoice.AdditionalInformation}\n");
            
            Console.WriteLine("Tuote\t\t\tMäärä\t\t\tYksikkö\t\t\tA-hinta\t\t\tYhteensä");
            
            invoice.InvoiceLineList.GetInvoiceLineList().ForEach(invoiceLine => Console.WriteLine(invoiceLine));
            
            Console.WriteLine($"YHTEENSÄ: {invoice.Sum}");
            
            Console.WriteLine($"\n{new String('*', Console.WindowWidth)}\n");
        }

        static void PrintAllInvoices()
        {
            Console.Clear();

            var invoiceList = InvoiceListRepo.GetInvoiceList();

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

        static void PrintInvoiceBasedOnNumber()
        {
            Console.Clear();

            var invoiceList = InvoiceListRepo.GetInvoiceList();

            if (invoiceList.Count > 0)
            {
                while (true)
                {
                    var id = ValidateInt("Anna laskun numero: ");

                    if (id > invoiceList.Count)
                    {
                        Console.WriteLine("Tällä numerolla ei löydy laskua\n");
                        
                        Console.WriteLine("Haluatko kokeilla uudestaan? (k/e)");

                        var key = Console.ReadKey(true).Key;

                        if (key == ConsoleKey.K)
                        {
                            continue;
                        }
                        else if (key == ConsoleKey.E)
                        {
                            break;
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

        static void PrintInvoiceBasedOnCustomer()
        {
            Console.Clear();

            var invoiceList = InvoiceListRepo.GetInvoiceList();

            if (invoiceList.Count > 0)
            {
                while (true)
                {
                    var customerName = ValidateString("Anna asiakkaan nimi: ");

                    if (invoiceList.Any(invoice => invoice.Customer.Name == customerName))
                    {
                        PrintInvoice(invoiceList.First(invoice => invoice.Customer.Name == customerName));

                        ReturnToMainMenu();

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Tällä nimellä ei löydy laskua\n");

                        Console.WriteLine("Haluatko kokeilla uudestaan? (k/e)");

                        var key = Console.ReadKey(true).Key;

                        if (key == ConsoleKey.K)
                        {
                            continue;
                        }
                        else if (key == ConsoleKey.E)
                        {
                            break;
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

        static void PrintInvoicesBasedOnProduct()
        {
            Console.Clear();
            
            var productList = ProductListRepo.GetProductList();
            
            var invoiceList = InvoiceListRepo.GetInvoiceList();

            if (invoiceList.Count > 0)
            {
                int productID;

                while (true)
                {
                    productList.ForEach(product =>
                    {
                        Console.WriteLine($"{product.ID}.\t\t{product.ProductName}\n");
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
                        invoiceList.ForEach(invoice =>
                        {
                            var invoiceLineList = invoice.InvoiceLineList.GetInvoiceLineList();

                            if (invoiceLineList.Any(invoiceLine => invoiceLine.Product.ID == productID))
                            {
                                PrintInvoice(invoice);
                            }
                        });
                    }
                }
            }
            else
            {
                Console.WriteLine("Laskutietokanta on tyhjä\n");

                ReturnToMainMenu();
            }
        }

        static void AddProduct()
        {
            while (true)
            {
                PrintAllProducts();

                Console.WriteLine("Haluatko lisätä uuden tuotteen? (k/e)");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.K)
                {
                    ProductListRepo.AddToProductList(CreateNewProduct());
                }
                else if (key == ConsoleKey.E)
                {
                    break;
                }
            }
        }

        static void PrintAllProducts()
        {
            Console.Clear();

            var separator = $"\n{new String('*', 50)}\n";

            var productList = ProductListRepo.GetProductList();

            if (productList.Count > 0)
            {
                Console.WriteLine($"Tuotelistalla olevat tuotteet\n{separator}");
                
                productList.ForEach(product => { Console.Write($"Tuote: {product.ProductName}\t\tHinta: {product.Price} e / {product.Unit}\n{separator}\n"); } );
            }
            else
            {
                Console.WriteLine("Tuotetietokanta on tyhjä\n");
            }
        }

        static Product CreateNewProduct()
        {
            var productName = ValidateString("Anna tuotteen nimi: ");
            
            var unit = ValidateString("Anna tuotteen yksikkö: ");

            var price = ValidateDouble("Anna tuotteen hinta: ");

            return new Product(productName, unit, price);
        }

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

            while (true)
            {
                Console.Clear();

                Console.CursorVisible = false;

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
                    // Some method
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
                else if(key == ConsoleKey.D4)
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