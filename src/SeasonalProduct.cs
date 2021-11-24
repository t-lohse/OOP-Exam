using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Stregsystem
{
    class SeasonalProduct : Product
    {
        DateTime SeasonStartDate {get; }
        DateTime SeasonEndDate {get; }
        public SeasonalProduct(uint id, string name, uint price, bool active, bool credit, DateTime startDate, DateTime endDate)
            : base(id, name, price, active, credit)
        {
            SeasonStartDate = startDate;
            SeasonEndDate = endDate;
        }
    }
}
