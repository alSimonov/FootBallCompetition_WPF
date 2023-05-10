 using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.Pages;
using FootBallCompasition_WPF.Pages.pgsMatch;
using FootBallCompasition_WPF.Pages.pgsParticipant;
using FootBallCompasition_WPF.Pages.pgsStadium;
using FootBallCompasition_WPF.Pages.pgsTeam;
using FootBallCompasition_WPF.UserControls.SideMenu;
using FootBallCompasition_WPF.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;

namespace FootBallCompasition_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainDBContext? _db;


        public MainWindow()
        {
            InitializeComponent();


            //tglbtnWindowScreen.Click += tglbtnWindowScreen_Click;


            var dialog = new WindowAuthorization();
            if(dialog.ShowDialog() == false)
            {
                Close();

            }
            else     
            {
                dbConfiguration.ConfigureServices();
                _db = dbConfiguration.Services.GetService<MainDBContext>();

            }


            //var menuExpenses = new List<SubItem>();
            //menuExpenses.Add(new SubItem("Cash flow", new PageParticipant("Игрок")));
            ////var item1 = new ItemMenu("Financial", menuExpenses, iconMaterial: MahApps.Metro.IconPacks.PackIconMaterialKind.Cog);
            //var item1 = new ItemMenu("Financial", menuExpenses);



            //stcSideMenu.Children.Add(new uscMenuItem(item1, this));




        }

        //internal void SwitchScreen(object sender)
        //{
        //    //var screen = ((UserControl)sender);

        //    var screen = ((Page)sender);
        

        //    if(screen != null)
        //    {
        //        //stcSideMenu.Children.Clear();
        //        //stcSideMenu.Children.Add(screen);

        //        frame.Navigate(screen);

        //    }
        
        
        
        //}
        



        private void btnFrameTeam_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageTeam());
        }

        private void btnFrameParticipant_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageParticipant("Игрок"));
        }

        private void btnFrameStadium_Click(object sender, RoutedEventArgs e)
        {

            frame.Navigate(new PageStadium());
        }

        private void btnFrameMatch_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageMatch());
        }

        private void btnFrameAccount_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageAccount());
        }

        private void btnFrameStatistic_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageStatistic());
        }

        //private void tglbtnWindowScreen_Click(object sender, RoutedEventArgs e)
        //{
        //    if()
        //    WindowState = WindowState.Maximized;
        //    WindowStyle = WindowStyle.None;
        //}



        private void btnWindowClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void btnWindowHide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
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



            //Application.Current.Resources.Clear();


            ////Подгрузка основного стиля

            //string dict = $"Dictionaries/{NameTheme}";
            //var uriDict = new Uri(dict + ".xaml", UriKind.Relative);


            //ResourceDictionary resourceDictionary = Application.LoadComponent(uriDict) as ResourceDictionary;

            //Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);




            //Подгрузка темы

            string style = $"Dictionaries/{NameTheme}";
            var uriStyle = new Uri(style + ".xaml", UriKind.Relative);


            ResourceDictionary resourceTheme = System.Windows.Application.LoadComponent(uriStyle) as ResourceDictionary;


            System.Windows.Application.Current.Resources.MergedDictionaries.Add(resourceTheme);




        }
    }
}
