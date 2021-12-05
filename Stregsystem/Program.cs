using System;
using System.IO;

namespace Stregsystem
{
    static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            
            User u = new("Gamer Pige", "user", "namer@domain.com", null);
            User p = new("Gamer boi", "user", "namer@domain.com", null);
            Console.WriteLine($"{u} - {u.Id}");
            Console.WriteLine($"{p} - {p.Id}");
            Console.WriteLine(new FileInfo(".").FullName);
            
            Stregsystem stregsystem = new Stregsystem();
            foreach (var t in stregsystem.Transactions)
            {
                Console.WriteLine(t.ToString());
            }
            User l = new User("First Last", "test", "username@domain.dk", 12);
            foreach (var t in stregsystem.GetTransactions(l, 3))
            {
                Console.WriteLine(t.ToString());
            }
        }
    }
}
