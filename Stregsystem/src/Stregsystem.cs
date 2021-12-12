using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using Stregsystem.Exceptions;
using Stregsystem.Interfaces;


namespace Stregsystem
{
    ///<summary>The Stregsystem class, which implements the IStregsystem interface.</summary>
    public class Stregsystem : IStregsystem
    {
        public List<Product> Products { get; }
        public List<Transaction> Transactions { get; }
        public List<User> Users { get; }
        public List<Product> ActiveProducts => Products.Where(x => x.Active).ToList();

        private readonly string _logPath;
        private readonly string _userPath;
        private readonly string _productPath;

        public event UserBalanceNotification? LowBalance;

        private const float WarningAmount = 50;


        ///<param name="logPath">Path for the file containing transaction-logs.</param>
        ///<param name="userPath">Path for the file containing all <c>User</c>s registered
        ///in the Stregsystem.</param>
        ///<param name="productPath">Path for the file containing all <c>Product</c>s registered
        ///in the Stregsystem.</param>
        ///<summary>The constructor reads, and pulls from the given file-paths.</summary>
        public Stregsystem(string logPath = "./transaction_log.csv", string userPath = "./users.csv",
                string productPath = "./products.csv")
        {
            Transactions = new List<Transaction>();
            Products = new List<Product>();
            Users = new List<User>();

            this._logPath = logPath;
            this._userPath = userPath;
            this._productPath = productPath;

            ReadData();
        }

        ///<summary>Reads data from files (<c>userPath</c> and <c>productPath</c>).</summary>
        private void ReadData()
        {
            if (!File.Exists(_userPath))
            {
                StreamWriter fs = File.CreateText(_userPath);
                fs.WriteLine("id;date;username;amount;product_id");
                fs.Flush();
                goto _Products;
            }
            List<string> users = File.ReadAllLines(_userPath).ToList();
            List<string> formattingUser = users[0].Split(',').ToList();
            users.RemoveAt(0);
            foreach (string[] s in users.Select(x => x.Split(',')))
            {
                Users.Add(new User(
                    name: $"{s[formattingUser.IndexOf("firstname")]} {s[formattingUser.IndexOf("lastname")]}",
                    username: s[formattingUser.IndexOf("username")],
                    email: s[formattingUser.IndexOf("email")],
                    id: uint.Parse(s[formattingUser.IndexOf("id")]),
                    initBalance: int.Parse(s[formattingUser.IndexOf("balance")])
                ));
            }

            _Products:
            if (!File.Exists(_productPath))
            {
                StreamWriter fs = File.CreateText(_productPath);
                fs.WriteLine("id;date;username;amount;product_id");
                fs.Flush();
                goto _History;
            }
            List<string> products = File.ReadAllLines(_productPath).ToList();
            List<string> formattingProducts = products[0].Split(';').ToList();
            products.RemoveAt(0);
            foreach (string[] str in products.Select(x => x.Split(';')))
            {
                string[] s = str.Select(x => x.Trim('"'))
                    .Select(x => Regex.Replace(x, "<(.*?)>", string.Empty))
                    .ToArray();

                if (string.IsNullOrWhiteSpace(s[formattingProducts.IndexOf("deactivate_date")]))
                    Products.Add(new Product(
                        name: s[formattingProducts.IndexOf("name")],
                        price: float.Parse(s[formattingProducts.IndexOf("price")]),
                        active: s[formattingProducts.IndexOf("active")] == "1",
                        id: uint.Parse(s[formattingProducts.IndexOf("id")])
                    ));
                else
                    Products.Add(new SeasonalProduct(
                        name: s[formattingProducts.IndexOf("name")],
                        price: float.Parse(s[formattingProducts.IndexOf("price")]),
                        active: s[formattingProducts.IndexOf("active")] == "1",
                        id: uint.Parse(s[formattingProducts.IndexOf("id")]),
                        endDate: DateTime.ParseExact(s[formattingProducts.IndexOf("deactivate_date")],
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                ));
            }
            _History:
            if (!File.Exists(_logPath))
            {
                StreamWriter fs = File.CreateText(_logPath);
                fs.WriteLine("id;date;username;amount;product_id");
                fs.Flush();
                return;
            }
            List<string> transactions = File.ReadAllLines(_logPath).ToList();
            List<string> formattingTransactions = transactions[0].Split(';').ToList();
            transactions.RemoveAt(0);
            transactions.Sort((x, y) =>
                int.Parse(x.Split(';')[formattingTransactions.IndexOf("id")]) - 
                int.Parse(y.Split(';')[formattingTransactions.IndexOf("id")]));
            foreach (string[] str in transactions.Select(x => x.Split(';')))
            {
                string[] s = str.Select(x => x.Trim('"'))
                    .Select(x => Regex.Replace(x, "<(.*?)>", string.Empty))
                    .ToArray();

                if (string.IsNullOrWhiteSpace(s[formattingTransactions.IndexOf("product_id")]))
                    Transactions.Add(new InsertCashTransaction(
                        date: DateTime.ParseExact(s[formattingTransactions.IndexOf("date")],
                            "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                        user: GetUserByUsername(s[formattingTransactions.IndexOf("username")]),
                        amount: uint.Parse(s[formattingTransactions.IndexOf("amount")])
                    ));
                else
                    Transactions.Add(new BuyTransaction(
                        date: DateTime.ParseExact(s[formattingTransactions.IndexOf("date")],
                            "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                        user: GetUserByUsername(s[formattingTransactions.IndexOf("username")]),
                        product: GetProductById(uint.Parse(s[formattingTransactions.IndexOf("product_id")]))
                    ));
            }
        }

        ///<param name="user">The <c>User</c> (attempting) to make a purchase.</param>
        ///<param name="product">The <c>Product</c> the user is (attempting) to purchase.</param>
        ///<summary>Method for creating the <c>Transaction</c> for buying a <c>Product</c>.</summary>
        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction _out = new BuyTransaction(user, DateTime.Now, product);
            ExecuteTransaction(_out);
            OnBalanceDecrement(user);
            return _out;
        }

        ///<param name="user">The <c>User</c> whose account is being deposited to.</param>
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

            using (StreamWriter file = File.AppendText(_logPath))
            {
                file.WriteLine(transaction.ToString());
                file.Flush();
            }

            List<string> existing = File.ReadLines(_userPath).ToList();

            int index = existing[0].Split(',').ToList().IndexOf("id");
            
            string line = existing.First(x
                => x.Split(',').ToList()[index] == transaction.User.Id.ToString());
            existing.Remove(line);
            List<string> lines = line.Split(',').ToList();
            lines[existing[0].Split(',').ToList().IndexOf("balance")]
                = transaction.User.Balance.ToString();
            existing.Add(string.Join(',', lines));
            File.WriteAllLines(_userPath, existing);
        }

        ///<param name="user">The <c>User</c> whose balance is being checked.</param>
        ///<summary>Method for notifying the user when their balance is low.</summary>
        private void OnBalanceDecrement(User user)
        {
            if (user.Balance <= WarningAmount)
            {
                LowBalance.Invoke(user, (decimal)user.Balance);
            }
        }

        ///<param name="id">The product being searched for's id.</param>
        ///<returns>The <c>Product</c> being search for. If not found, it throws an exception.</returns>
        ///<summary>Method for getting a specific <c>Product</c> by its id.</summary>
        public Product GetProductById(uint id)
        {
            var temp = Products.FirstOrDefault(x => x.Id == id);
            if (temp == null)
                throw new NonExistingProductException(id);
            return temp;
        }

        ///<param name="predicate">A <c>Func</c>-delegate, for determining what <c>User</c>s to
        ///return.</param>
        ///<returns>A <c>List</c> of all <c>User</c>s, that upholds the <c>predicate</c>.</returns>
        ///<summary>Method for getting users, upholding a specified parameter.</summary>
        public List<User> GetUsers(Func<User, bool> predicate) => Users.Where(x => predicate(x)).ToList();

        ///<param name="username">The username to search for</param>
        ///<returns>The <c>User</c> whose username matches <c>username</c>.</returns>
        ///<summary>Method for getting a <c>User</c> by a username.</summary>
        public User GetUserByUsername(string username)
        {
            try
            {
                return Users.First(x => x.Username == username);
            }
            catch
            {
                throw new InvalidUsernameException(username);
            }
        }

        ///<param name="user">The <c>User</c>, whose history is being searched.</param>
        ///<param name="count">The count of transaction to get (Latest first).</param>
        ///<returns>A <c>List</c> of the last <c>count</c><c>Transaction</c>s the given
        ///<c>user</c> has made. If there is less than <c>count</c>, they will be returned.</returns>
        ///<summary>Method for getting transactions from a user.</summary>
        public List<Transaction> GetTransactions(User user, int count) =>
            Transactions.Where(x => x.User.Equals(user))
            .OrderByDescending(x => x.Id)
            .Take(count)
            .ToList();
    }
}
