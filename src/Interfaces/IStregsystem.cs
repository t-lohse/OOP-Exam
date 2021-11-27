using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    ///<summary>Interface specifying the methods needed for the Stregsystem to work.</summary>
    interface IStregsystem 
    { 
       List<Product> ActiveProducts { get; } 
       void AddCreditsToAccount(User user, float amount); 
       void BuyProduct(User user, Product product); 
       Product GetProductByID(uint id); 
       IEnumerable<Transaction> GetTransactions(User user, int count); 
       IEnumerable<User> GetUsers(Func<User, bool> predicate); 
       User GetUserByUsername(string username); 
    }
}
