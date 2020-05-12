using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Display.ViewModels
{
    public class DataTableAjaxColumn
    {
#pragma warning disable IDE1006 // Naming Styles
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public bool isvisible { get; set; }
        public DataTableAjaxSearch search { get; set; }
        public string format { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}