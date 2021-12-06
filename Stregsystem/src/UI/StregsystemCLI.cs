using System;
using Stregsystem.Interfaces;

namespace Stregsystem.UI
{   
    public class StregsystemCli : IStregsystemUi
    {
        
        public event StregsystemEvent CommandEntered;
        private Stregsystem _sts;
        private bool _running;

        public StregsystemCli(Stregsystem sts)
        {
            _sts = sts;
        }

        public void Start()
        {
            if (CommandEntered.Target == null)
                return;
            Console.Clear();
            _running = true;
            while (_running)
            {
                Console.Write("\n> ");
                CommandEntered.Invoke(Console.ReadLine());
            }
        }
        
        public void Close()
        {
            _running = false;
            Console.WriteLine("Bye");
        }

        public void DisplayUserNotFound(string username) => Console.WriteLine($"User '{username}' not found!");

        public void DisplayProductNotFound(string product) => Console.WriteLine($"Product '{product}' not found!");

        public void DisplayUserInfo(User user) => Console.WriteLine($@"
{user.FirstName} {user.LastName} ({user.Username}) has {user.Balance} credits!");

        public void DisplayTooManyArgumentsError(string command) => Console.WriteLine($"Too many arguments for command '{command}'!");

        public void DisplayAdminCommandNotFoundMessage(string adminCommand) => Console.WriteLine($"Admin-command '{adminCommand}' not found!");

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine($"User {transaction.User.Username} has bought 1 '{transaction.Product.Name}' for the price of {transaction.Amount} credits!");
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine($"User {transaction.User.Username} has bought {count} '{transaction.Product.Name}' for the price of {transaction.Amount * count} credits!");
        }

        public void DisplayInsufficientCash(User user, float price)
        {
            Console.WriteLine($"Insufficient funds! You currently have {user.Balance}, the price for your purchase is {price}.");
        }

        public void DisplayGeneralError(string errorString) => Console.WriteLine(errorString);
    }
}