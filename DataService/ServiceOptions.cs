using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService
{
    public class ServiceOptions
    {

        public string BaseAddress { get; set; }

        public string PrismUrl { get; set; }

        /// <summary>
        /// Prism DB Connection string loaded from appSettings.json
        /// </summary>
        public string PrismDBConnectionString { get; set; }

        /// <summary>
        /// Authentication DB connection string loaded from appSettings.json
        /// </summary>
        public string AuthenticationDBConnectionString { get; set; }

        /// <summary>
        /// Determines whether the app runs using the In Memory Reepository (True) or the DB repository (False)
        /// </summary>
        public bool UseInMemoryRepository { get; set; }
    }
}
