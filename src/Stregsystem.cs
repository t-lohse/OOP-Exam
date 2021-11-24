using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    class Stregsystem : IStregsystem
    {
        public List<Product> Products { get; }
        public List<Transaction> Transactions { get; }
        public List<User> Users { get; }
        public event UserBalanceNotification UserBalanceWarning; 
        public List<Product> ActiveProducts
        {
            get => Products.Where(x => x.Active).ToList();
        }

        private FileInfo logFile;
        private FileInfo userFile;
        private FileInfo productFile;

        public Stregsystem(string logPath = "./History.log", string userPath = "./Users.csv",
                string productPath = "./Products.csv")
        {
            Transactions = new List<Transaction>();
            try
            {
                logFile = new FileInfo(logPath);
            }
            catch
            {
                using (File.Create(logPath))
                logFile = new FileInfo(logPath);
                Debug.WriteLine("Created File");
            }
        }

        public void BuyProduct(User user, Product product)
        {
            ExecuteTransaction(new BuyTransaction(user, DateTime.Now, product.Price, product));
            OnBalanceDecrement(user);
        }

        public void AddCreditsToAccount(User user, float amount)
        {
            ExecuteTransaction(new InsertCashTransaction(user, DateTime.Now, amount));
        }

        public void ExecuteTransaction(Transaction transaction) //TODO: Logging
        {
            Transactions.Add(transaction);
            transaction.Execute();
            
            using (StreamWriter file = logFile.AppendText()) {
                file.WriteLine(transaction.ToString());
                file.Flush();
            }
            
        }

        private void OnBalanceDecrement(User user) {
            if (user.Balance < 50)
                UserBalanceWarning(user, (decimal)user.Balance);
        }

        // TODO: "Brugerdefineret" Exception
        public Product GetProductByID(uint id) => Products.First(x => x.ID == id);

        public IEnumerable<User> GetUsers(Func<User, bool> predicate) => Users.Where(x => predicate(x)).ToList();
        public User GetUserByUsername(string username) => Users.First(x => x.UserName == username);
        public IEnumerable<Transaction> GetTransactions(User user, int count) =>
            Transactions.Where(x => x.User.Equals(user))
            .Take(count)
            .ToList();
    }
}
