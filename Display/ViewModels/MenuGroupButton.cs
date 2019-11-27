using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Display.ViewModels
{
    public class MenuGroupButton
    {
        public string ButtonText { get; set; }
        public string ButtonTextNoSpace
        {
            get
            {
                return ButtonText.Replace(" ", "_");
            }
        }
        public bool IsDisabled { get; set; }
        public string ToolTip { get; set; }
        public List<MenuButton> MenuButtons { get; set; }
    }
}
