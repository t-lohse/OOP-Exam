using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    ///<summary>Class specifying whenever a transaction occurs.</summary>
    abstract class Transaction
    {
        public int ID { get; }
        private static int id = 1;
        public User User { get; }
        public DateTime Date { get; }
        public float Amount { get; }

        ///<summary>Class specifying whenever a transaction occurs.</summary>
        public Transaction(User user, DateTime date, float amount)
        {
            ID = id;
            User = user;
            Date = date;
            Amount = amount;

            ID = id++;
        }

        public abstract void Execute();

        public abstract string ToString();
    }
}
