using System;
using System.Collections.Generic;

namespace Stregsystem
{
    ///<summary>Interface specifying the methods needed for the Stregsystem to work.</summary>
    interface IStregsystem 
    { 
       List<Product> ActiveProducts { get; } 
       void AddCreditsToAccount(User user, float amount); 
       void BuyProduct(User user, Product product); 
       Product GetProductByID(uint id); 
       List<Transaction> GetTransactions(User user, int count); 
       List<User> GetUsers(Func<User, bool> predicate); 
       User GetUserByUsername(string username); 
    }
}
