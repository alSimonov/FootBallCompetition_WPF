using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FootBallCompasition_WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для windowConfirmation.xaml
    /// </summary>
    public partial class windowConfirmation : Window
    {

        public windowConfirmation()
        {
            InitializeComponent();

        }

        public windowConfirmation(string info)
        {
            InitializeComponent();

            tblInfo.Text = info;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
