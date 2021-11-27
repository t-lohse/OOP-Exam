using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    ///<summary>Exception thrown when searching for a product that does not exist.</summary>
    class NonExistingProductException : Exception
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
