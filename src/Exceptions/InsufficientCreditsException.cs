using System;

namespace Stregsystem
{
    ///<summary>Exception thrown when trying to pay for a product with an insufficient amount of
    ///credit.</summary>
    class InsufficientCreditsException : Exception
    {
        public User User { get; }
        public Product Product { get; }
        ///<param name="user">The <c>User</c> attempting the pruchase.</param>
        ///<param name="product">The <c>Product</c> the specified user is trying to purchase.</param>
        ///<summary>Exception thrown when trying to pay for a product with an insufficient amount of
        ///credit.</summary>
        public InsufficientCreditsException(User user, Product product)
            : base("Not enough credits.")
        {
            User = user;
            Product = product;
        }
    }
}
