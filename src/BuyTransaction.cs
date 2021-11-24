using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    class BuyTransaction : Transaction
    {
        public Product Product { get; }
        public BuyTransaction(User user, DateTime date, float amount, Product product) : base(user, date, amount)
        {
            Product = product;
        }

        public override void Execute() 
        {
            if (!Product.CanBeBoughtOnCredit && User.Balance < Amount)
                throw new InsufficientCreditsException("Not enough credits", User, Product);
            User.Balance -= Amount;
        }

        public override string ToString() => $"Transaction {ID} ({Date.ToString()}): {User.UserName} spent {Amount.ToString()}.";
    }
}
