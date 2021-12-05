using System;
using System.Collections.Generic;
using System.Linq;

namespace Stregsystem.Exceptions
{
    ///<summary>Exception thrown when trying to pay for a product with an insufficient amount of
    ///credit.</summary>
    public class InsufficientCreditsException : Exception
    {
        public User User { get; }
        public List<Product> Cart { get; } = new List<Product>();
        public float Total => Cart.Sum(x => x.Price);
        ///<param name="user">The <c>User</c> attempting the purchase.</param>
        ///<param name="product">The <c>Product</c> the specified user is trying to purchase.</param>
        ///<summary>Exception thrown when trying to pay for a product with an insufficient amount of
        ///credit.</summary>
        public InsufficientCreditsException(User user, Product product)
            : base("Not enough credits.")
        {
            User = user;
            Cart.Add(product);
        }
        
        ///<param name="user">The <c>User</c> attempting the purchase.</param>
        ///<param name="products">The <c>Product</c>s the specified user is trying to purchase.</param>
        ///<summary>Exception thrown when trying to pay for a product with an insufficient amount of
        ///credit.</summary>
        public InsufficientCreditsException(User user, List<Product> products)
            : base("Not enough credits.")
        {
            User = user;
            Cart = products;
        }
    }
}
