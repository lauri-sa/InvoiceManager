namespace Harjoitustyo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                Console.CursorVisible = false;

                Console.WriteLine("Laskutussovellus\n");

                Console.WriteLine("1 = Lisää lasku\n");

                Console.WriteLine("2 = Tulosta kaikki laskut\n");

                Console.WriteLine("3 = Tulosta kaikki tuotteet\n");

                Console.WriteLine("4 = Tulosta lasku numeron perusteella\n");

                Console.WriteLine("5 = Tulosta lasku asiakkaan perusteella\n");

                Console.WriteLine("6 = Tulosta laskut tuotteen perusteella\n");

                Console.Write("Esc = Sulje ohjelma");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.D1)
                {
                    // Some method
                }
                else if (key == ConsoleKey.D2)
                {
                    // Some method
                }
                else if (key == ConsoleKey.D3)
                {
                    // Some method
                }
                else if(key == ConsoleKey.D4)
                {
                    // Some method
                }
                else if (key == ConsoleKey.D5)
                {
                    // Some method
                }
                else if (key == ConsoleKey.D6)
                {
                    // Some method
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}