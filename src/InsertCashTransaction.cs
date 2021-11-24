using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, DateTime date, float amount) : base(user, date, amount)
        {
            
        }

        public override void Execute() => User.Balance += Amount;

        public override string ToString() => $"Transaction {ID} ({Date.ToString()}): {User.UserName} insterted {Amount.ToString()}.";
    }
}
