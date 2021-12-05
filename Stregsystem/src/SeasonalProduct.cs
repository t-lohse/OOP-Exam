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
        
        ///<param name="id">The id of the product.</param>
        ///<param name="name">The name of the product.</param>
        ///<param name="price">The price of the product.</param>
        ///<param name="endDate">The date of which the product will be inactive (Out of season).</param>
        ///<param name="active">The state the product is in. If it is active, it can be bought.</param>
        ///<summary>Constructor, taking the products id, name, price, and status' as arguments.
        ///Beyond the standard arguments, it takes the timespan the product is available in.</summary>
        public SeasonalProduct(uint id, string name, float price, DateTime endDate, bool active = true)
            : base(id, name, price, active)
        {
            SeasonEndDate = endDate;
        }
    }
}
