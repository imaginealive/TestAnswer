using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class InterestData
    {
        public int YearCount { get; set; }
        public double Principle { get; set; }
        public double Interest { get; set; }
        public double Total { get; set; }
    }
}
