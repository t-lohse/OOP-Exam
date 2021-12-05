using System;
using System.Collections.Generic;

namespace Stregsystem.Interfaces
{
    ///<summary>Interface specifying the methods needed for the Stregsystem to work.</summary>
    public interface IStregsystem 
    {
       List<Product> ActiveProducts { get; } 
       
       ///<param name="user">The <c>User</c> whose account is being deposited to.</param>
       ///<param name="amount">The amount of money being deposited.</param>
       ///<summary>Method for adding credit to <c>user</c>.</summary>
       void AddCreditsToAccount(User user, float amount); 
       
       ///<param name="user">The <c>User</c> (attempting) to make a purchase.</param>
       ///<param name="product">The <c>Product</c> the user is (attempting) to purchase.</param>
       ///<summary>Method for buying a <c>product</c> by <c>user</c>.</summary>
       BuyTransaction BuyProduct(User user, Product product); 
       
       ///<param name="id">The product being searched for's id.</param>
       ///<returns>The <c>Product</c> being search for. If not found, it throws an exception.</returns>
       ///<summary>Method for getting a specific <c>Product</c> by its id.</summary>
       Product GetProductById(uint id); 
       
       ///<param name="user">The <c>User</c>, whose history is being searched.</param>
       ///<param name="count">The count of transaction to get (Latest first).</param>
       ///<returns>A <c>List</c> of the last <c>count</c><c>Transaction</c>s the given
       ///<c>user</c> has made. If there is less than <c>count</c>, they will be returned.</returns>
       ///<summary>Method for getting transactions from a specific user.</summary>
       List<Transaction> GetTransactions(User user, int count); 
       
       ///<param name="predicate">A <c>Func</c>-delegate, for determining what <c>User</c>s to
       ///return.</param>
       ///<returns>A <c>List</c> of all <c>User</c>s, that upholds the <c>predicate</c>.</returns>
       ///<summary>Method for getting users, upholding a specified parameter (<c>predicate</c>).</summary>
       List<User> GetUsers(Func<User, bool> predicate); 
       
       ///<param name="username">The username to search for</param>
       ///<returns>The <c>User</c> whose username matches <c>username</c>.</returns>
       ///<summary>Method for getting a <c>User</c> by a username.</summary>
       User GetUserByUsername(string username); 
    }
}
