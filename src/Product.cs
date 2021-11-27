using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    ///<summary>A product that either has been, or is in the Stregsystems catalog.</summary>
    class Product
    {
        public uint ID { get; }
        public string Name { get; set; }
        public float Price { get; set; }
        public bool Active { get; }
        public bool CanBeBoughtOnCredit { get; set; }

        ///<summary>Constructor, taking the products id, name, price, and stutus' as arguments</summary>
        public Product(uint id, string name, float price, bool active, bool credit)
        {
            ID = id;
            Name = name;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = credit;
        }

        ///<summary>The <c>ToString</c>-method used for when printing the product in the UI</summary>
        public override string ToString() => $"{ID} | {Name} | {Price}";
    }
}
