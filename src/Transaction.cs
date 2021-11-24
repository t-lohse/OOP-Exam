using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    abstract class Transaction
    {
        public int ID { get; }
        private static int id = 1;
        public User User { get; }
        public DateTime Date { get; }
        public float Amount { get; }

        public Transaction(User user, DateTime date, float amount)
        {
            ID = id;
            User = user;
            Date = date;
            Amount = amount;

            ID = id++;
        }

        //TODO: Error-handle and log
        public abstract void Execute();

        //TODO: reformulate string
        public override string ToString() => $"Transaction {ID}: {User.FirstName} spent {Amount.ToString()} on {Date.ToString()}";
    }
}
