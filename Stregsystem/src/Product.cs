using System;
using Stregsystem.Exceptions;

namespace Stregsystem
{
    ///<summary>A product that either has been, or is in the Stregsystems catalog.</summary>
    public class Product : IComparable
    {
        public uint Id { get; }
        public string Name { get; }
        private float _price;
        public float Price {
            get => _price;
            set => _price = value > 0 ? value : 0;
        }
        public bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; } = false;

        ///<param name="id">The id of the product.</param>
        ///<param name="name">The name of the product.</param>
        ///<param name="price">The price of the product.</param>
        ///<param name="active">The state the product is in. If it is active, it can be bought.</param>
        ///<summary>Constructor, taking the products id, name, price, and status' as arguments</summary>
        public Product(uint id, string name, float price, bool active = true)
        {
            if (id < 1)
                throw new InvalidIdException();
            Id = id;
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidNameException();
            Name = name;
            Price = price;
            Active = active;
        }

        ///<summary>The <c>ToString</c>-method used for when printing the product in the UI</summary>
        public override string ToString() => $"{Id} | {Name} | {Price}";
        
        public int CompareTo(object? obj) {
            if (obj == null)
                return 1;
            Product other = obj as Product;
            return (int)(Id - other.Id);
        }
    }
}
