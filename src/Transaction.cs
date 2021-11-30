using System;

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

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public abstract string ToString();
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
    }
}
