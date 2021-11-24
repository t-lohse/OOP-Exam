using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    class Product
    {
        public uint ID { get; }
        public string Name { get; set; }
        public float Price { get; set; }
        public bool Active { get; }
        public bool CanBeBoughtOnCredit { get; set; }

        public Product(uint id, string name, float price, bool active, bool credit)
        {
            ID = id;
            Name = name;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = credit;
        }

        public override string ToString() => $"{ID} | {Name} | {Price}";
    }
}
