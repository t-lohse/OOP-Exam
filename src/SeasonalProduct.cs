using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    ///<summary>A derivitive of <c>Product</c>, specifying seasonal products
    ///(products that are only avaliable in a given span of time).
    ///</summary>
    class SeasonalProduct : Product
    {
        DateTime SeasonStartDate { get; }
        DateTime SeasonEndDate { get; }

        ///<summary>Constructor, taking the products id, name, price, and stutus' as arguments.
        ///Beyond the standard arguments, it takes the timespan the product is available in.</summary>
        public SeasonalProduct(uint id, string name, uint price, bool active, bool credit,
                               DateTime startDate, DateTime endDate)
            : base(id, name, price, active, credit)
        {
            SeasonStartDate = startDate;
            SeasonEndDate = endDate;
        }
    }
}
