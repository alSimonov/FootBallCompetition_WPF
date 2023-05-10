using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FootBallCompasition_WPF.UserControls.SideMenu
{
    public class SubItem
    {
        public string Name { get; private set; }
        //public UserControl Screen { get; private set; }

        public Page Screen { get; private set; }


        public SubItem(string name, Page screen = null)
        {
            Name = name;
            Screen = screen;
        }
    }
}
