using System;

namespace Stregsystem
{
    ///<summary>A derivitive of <c>Product</c>, specifying seasonal products
    ///(products that are only avaliable in a given span of time).
    ///</summary>
    public class SeasonalProduct : Product
    {
        DateTime SeasonStartDate { get; }
        DateTime SeasonEndDate { get; }

        ///<summary>Constructor, taking the products id, name, price, and stutus' as arguments.
        ///Beyond the standard arguments, it takes the timespan the product is available in.</summary>
        public SeasonalProduct(uint id, string name, float price, bool active, DateTime endDate)
            : base(id, name, price, active)
        {
            SeasonEndDate = endDate;
        }
    }
}
