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
    /// Логика взаимодействия для WindowAuthorization.xaml
    /// </summary>
    public partial class WindowAuthorization : Window
    {
        public WindowAuthorization()
        {
            InitializeComponent();

            btnLogin.Click += btnLogin_Click;

            btnWindowClose.Click += btnWindowClose_Click; 

        }

        private void btnWindowClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void tbLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            tbLogin.Text = String.Empty;
        }

        private void pbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pbPassword.Password = String.Empty;
        }
    }
}
