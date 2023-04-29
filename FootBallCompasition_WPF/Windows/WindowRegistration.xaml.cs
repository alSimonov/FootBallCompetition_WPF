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
    /// Логика взаимодействия для WindowRegistration.xaml
    /// </summary>
    public partial class WindowRegistration : Window
    {
        public WindowRegistration()
        {
            InitializeComponent();

            btnWindowClose.Click += BtnWindowClose_Click;
        
        
        }

        private void BtnWindowClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void tglbtnTheme_Click(object sender, RoutedEventArgs e)
        {
            
            string NameTheme;


            if (tglbtnTheme.IsChecked == true)
            {
                NameTheme = "DictionaryTheme2";


            }
            else 
            {
                NameTheme = "DictionaryTheme1";
            }



            Application.Current.Resources.Clear();


            ////Подгрузка основного стиля

            //string dict = $"Dictionaries/{NameTheme}";
            //var uriDict = new Uri(dict + ".xaml", UriKind.Relative);


            //ResourceDictionary resourceDictionary = Application.LoadComponent(uriDict) as ResourceDictionary;

            //Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);




            //Подгрузка темы

            string style = $"Dictionaries/{NameTheme}";
            var uriStyle = new Uri(style + ".xaml", UriKind.Relative);


            ResourceDictionary resourceTheme = Application.LoadComponent(uriStyle) as ResourceDictionary;


            Application.Current.Resources.MergedDictionaries.Add(resourceTheme);




        }
    }
}
