using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Shared
{
    public class PerformanceIndicator
    {
        public int ID { get; set; }
        public string Month { get; set; }
        public double PercentChange { get; set; }
    }
}
