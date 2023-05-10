using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.UserControls.SideMenu
{
    public class ItemMenu
    {
        public string Header { get; set; }
        //public PackIconMaterialKind IconMaterial { get; set; }
        public List<SubItem> SubItems { get; set; }
        //public UserControl Screen { get; set; }

        public ItemMenu(string header, List<SubItem> subItems)
        {
            Header = header;
            SubItems = subItems;
            
        }

        //public ItemMenu(string header, PackIconMaterialKind iconMaterial, UserControl screen)
        //{
        //    Header = header;
        //    Screen = screen;
        //    IconMaterial = iconMaterial;
        //}


    }
}
