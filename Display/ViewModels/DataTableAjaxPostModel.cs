using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Display.ViewModels
{
    public class DataTableAjaxPostModel
    {
        public DataTableAjaxPostModel()
        {
            length = 10;
            start = 0;
            search = new DataTableAjaxSearch()
            {
                value = "",
            };
            order = new List<DataTableAjaxOrder>()
            {
                new DataTableAjaxOrder()
                {

                },
            };
        }
        // properties are not capital due to json mapping
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<DataTableAjaxColumn> columns { get; set; }
        public DataTableAjaxSearch search { get; set; }
        public List<DataTableAjaxOrder> order { get; set; }
    }
}
