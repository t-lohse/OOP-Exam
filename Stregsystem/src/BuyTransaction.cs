using System;

namespace Stregsystem
{
    ///<summary>A specification of <c>Transaction<c>, specifying a "purchase" transaction.</summary>
    public class BuyTransaction : Transaction
    {
        public Product Product { get; }
        ///<param name="user">The <c>User</c> depositing the money to their "account".</param>
        ///<param name="date">The date of the deposit</param>
        ///<param name="amount">The amount deposited</param>
        ///<param name="prodcut">The <c>Product</c> the specified user is
        ///(attempting) to purchase.</param>
        ///<summary>Constructor for the class.</summary>
        public BuyTransaction(User user, DateTime date, float amount, Product product) : base(user, date, amount)
        {
            Product = product;
        }

        ///<summary>Method for executing the transaction (Spending).
        ///If there is an insufficient amount of credits in the users account,
        ///and the product can't be bought on credit, an exception will be thrown.</summary>
        public override void Execute() 
        {
            if (!Product.CanBeBoughtOnCredit && User.Balance < Amount)
                throw new InsufficientCreditsException(User, Product);
            User.Balance -= Amount;
        }

        ///<summary>The <c>ToString</c>-method, used for logging the transaction.</summary>
        public override string ToString() => $"Transaction {ID}: {User.UserName} spent {Amount.ToString()}.";
    }
}
