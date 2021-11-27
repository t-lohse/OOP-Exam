using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    ///<summary>The Stregsystem class, which implements the IStregsystem interface.</summary>
    class Stregsystem : IStregsystem
    {
        public List<Product> Products { get; }
        public List<Transaction> Transactions { get; }
        public List<User> Users { get; }
        public List<Product> ActiveProducts
        {
            get => Products.Where(x => x.Active).ToList();
        }

        private FileInfo logFile;
        private FileInfo userFile;
        private FileInfo productFile;


        ///<param name="logPath">Path for the file containing transaction-logs.</param>
        ///<param name="userPath">Path for the file containing all <c>User</c>s registered
        ///in the Stregsystem.</param>
        ///<param name="productPath">Path for the file containing all <c>Product</c>s registered
        ///in the Stregsystem.</param>
        ///<summary>The constructor reads, and pulls from the given file-paths.</summary>
        public Stregsystem(string logPath = "./History.log", string userPath = "./users.csv",
                string productPath = "./products.csv") // TODO: Files, and pull data from them
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

        ///<param name="user">The <c>User</c> (attempting) to make a purchase.</param>
        ///<param name="product">The <c>Product</c> the user is (attampting) to purchase.</param>
        ///<summary>Method for creating the <c>Transaction</c> for buying a <c>Product</c>.</summary>
        public void BuyProduct(User user, Product product)
        {
            ExecuteTransaction(new BuyTransaction(user, DateTime.Now, product.Price, product));
            OnBalanceDecrement(user);
        }

        ///<param name="user">The <c>User</c> whos account is being deposited to.</param>
        ///<param name="amount">The amount of money being deposited.</param>
        ///<summary>Method for creating the <c>Transaction</c> for
        ///depositing money to an <c>User</c>s account.</summary>
        public void AddCreditsToAccount(User user, float amount)
        {
            ExecuteTransaction(new InsertCashTransaction(user, DateTime.Now, amount));
        }

        ///<param name="transaction">The <c>Transaction</c> to be executed.</param>
        ///<summary>Method executing a <c>Transaction</c>, and logging it.</summary>
        public void ExecuteTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            transaction.Execute();
            
            using (StreamWriter file = logFile.AppendText()) {
                file.WriteLine(transaction.ToString());
                file.Flush();
            }
            
        }
        
        ///<param name="user">The <c>User</c> whos balance is being checked.</param>
        ///<summary>Method for notifying the user when their balance is low.</summary>
        private void OnBalanceDecrement(User user) {
            if (user.Balance < 50) { //TODO: Not const in-line, make const var
                // TODO: Notify
            }
        }

        ///<param name="id">The product being searched fors id.</param>
        ///<returns>The <c>Procudt</c> being search for. If not found, it throws an exception.</returns>
        ///<summary>Method for notifying the user when their balance is low.</summary>
        public Product GetProductByID(uint id) {
            var temp = Products.FirstOrDefault(x => x.ID == id);
            if (temp == null)
                throw new NonExistingProductException(id);
            return temp;
        }


        ///<param name="predicate">A <c>Func</c>-delegate, for determining what <c>User</c>s to
        ///return.</param>
        ///<returns>A <c>List</c> of all <c>User</c>s, that upholds the <c>predicate</c>.</returns>
        ///<summary>Method for getting users, upholding a specified parameter.</summary>
        public List<User> GetUsers(Func<User, bool> predicate) => Users.Where(x => predicate(x)).ToList();


        //TODO: Prob better error-handle
        ///<param name="username">The username to search for</param>
        ///<returns>The <c>User</c> whos username matches <c>username</c>.</returns>
        ///<summary>Method for getting a <c>User<c/> by a username.</summary>
        public User GetUserByUsername(string username) => Users.First(x => x.UserName == username);


        ///<param name="user">The <c>User<c>, whos history is being serached.</param>
        ///<param name="count">The count of transaction to get (Latest first).</param>
        ///<returns>A <c>List</c> of the last <c>count</c><c>Transaction</c>s the given
        ///<c>user</c> has made. If there is less than <c>count</c>, they will be returned.</returns>
        ///<summary>Method for getting transactions from a user.</summary>
        public List<Transaction> GetTransactions(User user, int count) =>
            Transactions.Where(x => x.User.Equals(user))
            .OrderByDescending(x => x.ID)
            .Take(count)
            .ToList();
    }
}
