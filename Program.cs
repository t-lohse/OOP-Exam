using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

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

            User u = new User("Gamer Pige", "GamerEn", "TEMP._-@1kl.c");
            Console.WriteLine($"{u.ToString()} - {u.ID}");


            IStregsystem stregsystem = new Stregsystem();
            stregsystem.BuyProduct(new User("Thomas Lohse", "lohse", "Gamer@gamer.dk", 200),
                    new Product(2, "bajs", 69, true, false));
        }
    }
}
