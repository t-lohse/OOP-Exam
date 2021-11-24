using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    class User : IComparable
    {
        public int ID { get; }
        private static int id = 1;
        public string FirstName { get; }
        public string LastName { get; }
        public string UserName { get; } //TODO: Fix for req
        public string Email { get; } //TODO: Validate
        public float Balance { get; set; }

        public User(string name, string username, string email, int initBalance = 0)
        {
            Email = ValidateEmail(email);
            string[] temp = name.Split(' ');
            LastName = temp[temp.Length - 1];
            Array.Resize(ref temp, temp.Length - 1);
            FirstName = String.Join(' ', temp);
            UserName = username;
            Balance = initBalance;
            
            ID = id++;
        }

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

        public int CompareTo(object? obj) {
            if (obj == null)
                return 1;
            User other = obj as User;
            return ID - other.ID;
        }
        public override string ToString() => $"{FirstName} {LastName} ({Email})";
        public bool Equals(User other) => ID == other.ID;
        public override int GetHashCode() => ID;
    }
}
