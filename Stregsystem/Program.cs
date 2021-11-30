using System;

namespace Stregsystem
{
    static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            /*
            User u = new User("Gamer Pige", "TEMP");
            User p = new User("Gamer boi", "TEMP");
            Console.WriteLine($"{u.ToString()} - {u.ID}");
            Console.WriteLine($"{p.ToString()} - {p.ID}");
            */
            IStregsystem stregsystem = new Stregsystem("../../....//history.log",
                "../../../../users.csv", "../../../../products.csv");
        }
    }
}
