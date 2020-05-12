using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Display
{
    public class ApplicationOptions
    {
        /// <summary>
        /// Holds the path to the Data directory
        /// </summary>
        public string DataDirectory { get; set; }

        /// <summary>
        /// Holds the path to the Images directory
        /// </summary>
        public string ImageDirectory { get; set; }


        public static string BaseAddress = "http://192.168.10.14:9810";

        public static string PrismUrl = BaseAddress + "/api/Prism/{0}";
    }
}
