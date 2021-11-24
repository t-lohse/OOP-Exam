using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    class InsufficientCreditsException : Exception
    {
        public User User { get; }
        public Product Product { get; }
        public InsufficientCreditsException(string desc, User user, Product product) : base(desc)
        {
            User = user;
            Product = product;
        }
    }
}
