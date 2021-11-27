using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    ///<summary>Class specifying a registered user in the Stregsystem.</summary>
    class User : IComparable
    {
        public uint ID { get; }
        private static uint _id = 1;
        public string FirstName { get; }
        public string LastName { get; }
        public string UserName { get; }
        public string Email { get; } 
        public float Balance { get; set; }

        ///<summary>Constructor for <c>User</c>. In here there occurs validation on the username,
        ///email, and full name. If pulling from a "database", the id should be inputted,
        ///if not, set it as null.</summary>
        public User(string name, string username, string email, uint? id, int initBalance = 0)
        {
            Email = ValidateEmail(email);

            string[] temp = name.Split(' ');
            if (temp.Length < 2)
                throw new Exception(); //TODO: Custom exception
            LastName = temp[temp.Length - 1];
            Array.Resize(ref temp, temp.Length - 1);
            FirstName = String.Join(' ', temp);

            UserName = ValidateUserName(username);
            Balance = initBalance;
            
            ID = _id++; //TODO: Make ID the top from reading
        }

        ///<param name="email">Email to validate.</param>
        ///<returns>A validated email.</returns>
        ///<summary>Method for validating the email.</summary>
        private string ValidateEmail(string email) {
            try
            {
                string[] temp = email.Split('@');
                if (temp.Length != 2)
                    throw new Exception();

                bool err = true;
                err &= temp[0].ToCharArray().ToList()
                    .All(c => Char.IsLetterOrDigit(c) ||
                            c == '.' || c == '-' || c == '_');

                err &= !(temp[1].StartsWith('-') || temp[1].EndsWith('-') ||
                        temp[1].StartsWith('.') || temp[1].EndsWith('.') || 
                        !temp[1].Contains('.') || temp[1].LastIndexOf('.') == temp[1].Length - 1);
                err &= temp[1].ToCharArray().ToList()
                    .All(c => Char.IsLetterOrDigit(c) ||
                            c == '-' || c == '.');

                if (!err)
                    throw new Exception();
            }
            catch // TODO: Custom throw exception 
            {
                throw new Exception("Invalid email");
            }

            return email;
        }

        ///<param name="username">The username to validate.</param>
        ///<returns>A validated username to lowercase.</returns>
        ///<summary>Method for validating a username.</summary>
        private string ValidateUserName(string username) {
            if (!username.ToCharArray().ToList()
                    .All(c => Char.IsLetterOrDigit(c) || c == '_'))
                throw new Exception(); //TODO: Custom Exception
            return username.ToLower();
        }

        public int CompareTo(object? obj) {
            if (obj == null)
                return 1;
            User other = obj as User;
            return (int)(ID - other.ID);
        }
        public override string ToString() => $"{FirstName} {LastName} ({Email})";
        public bool Equals(User other) => ID == other.ID;
        public override int GetHashCode() => (int)ID;
    }
}
