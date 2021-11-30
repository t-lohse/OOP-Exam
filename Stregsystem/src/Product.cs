namespace Stregsystem
{
    ///<summary>A product that either has been, or is in the Stregsystems catalog.</summary>
    public class Product
    {
        public uint ID { get; }
        public string Name { get; set; }
        private float _price;
        public float Price {
            get => _price;
            set => _price = value > 0 ? value : 0;
        }
        public bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }

        ///<summary>Constructor, taking the products id, name, price, and stutus' as arguments</summary>
        public Product(uint id, string name, float price, bool active)
        {
            ID = id;
            Name = name;
            Price = price;
            Active = active;
        }

        ///<summary>The <c>ToString</c>-method used for when printing the product in the UI</summary>
        public override string ToString() => $"{ID} | {Name} | {Price}";
    }
}
