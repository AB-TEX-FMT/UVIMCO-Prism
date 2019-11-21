using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Display.ViewModels
{
    public class MenuButton
    {
        public string ButtonText { get; set; }
        public bool IsDisabled { get; set; }
        public string ToolTip { get; set; }
        public string Url { get; set; }
    }
}
