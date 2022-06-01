using System;
using System.Collections.Generic;

namespace Customers_DAL.Entities
{
    public class PossibleTime
    {
        public TimeSpan Time { get; set; }
        
        public bool? Available { get; set; }
    }
}
