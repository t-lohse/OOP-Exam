using System;

namespace Stregsystem
{
    ///<summary>Class specifying whenever a transaction occurs.</summary>
    public abstract class Transaction
    {
        public int Id { get; }
        private static int _id = 1;
        public User User { get; }
        public DateTime Date { get; }
        public float Amount { get; }

        ///<param name="user">The <c>User</c> in question, making the transaction.</param>>
        ///<param name="date">The date of which the transaction occurs.</param>>
        ///<param name="amount">The amount of credit the transaction resolves around.</param>>
        ///<summary>Class specifying whenever a transaction occurs.</summary>
        public Transaction(User user, DateTime date, float amount)
        { 
            User = user;
            Date = date;
            Amount = amount > 0f ? amount : 0f;

            Id = _id++;
        }
        ///<summary>Method for executing/Registering the transaction.</summary>
        public abstract void Execute();

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public abstract string ToString();
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
    }
}
