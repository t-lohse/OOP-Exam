using System;

namespace Stregsystem
{
    ///<summary>Exception thrown when searching for a product that does not exist.</summary>
    public class NonExistingProductException : Exception
    {
        public uint Id { get; }
        ///<param name="id">The product-id searched for.</param>
        ///<summary>Exception thrown when searching for a product that does not exist.</summary>
        public NonExistingProductException(uint id)
            : base($"No product with ID {id}")
        {
            Id = id;
        }
    }
}
