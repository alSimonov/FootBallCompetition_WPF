 using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.Pages;
using FootBallCompasition_WPF.Pages.pgsMatch;
using FootBallCompasition_WPF.Pages.pgsParticipant;
using FootBallCompasition_WPF.Pages.pgsStadium;
using FootBallCompasition_WPF.Pages.pgsTeam;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using FootBallCompasition_WPF.UserControls.SideMenu;
using FootBallCompasition_WPF.Windows;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
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
    public partial class MainWindow : System.Windows.Window
    {


        public MainDBContext? _db;

        public int _idAccount;


        public MainWindow()
        {
            InitializeComponent();


            //tglbtnWindowScreen.Click += tglbtnWindowScreen_Click;


            Authorization();

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();



            //TODO убрать row с уведомлениями, настрокой и поиском  
            //TODO мб сделать менюшку с уведомлениями

        }



        private void Authorization()
        {

            this.Visibility = Visibility.Collapsed;

            var dialog = new WindowAuthorization();
            if (dialog.ShowDialog() == false)
            {
                Close();

            }
            else
            {

                if (dialog.TheAccountRole == "Админ")
                {
                    btnFrameAccount.Visibility = Visibility.Visible;
                }
                else
                {
                    btnFrameAccount.Visibility = Visibility.Collapsed;
                }

                tblFIOAccount.Text = dialog.FIOAccount;
                tblRoleAccount.Text = dialog.TheAccountRole;
                _idAccount = dialog.IdAccount;

                this.Visibility = Visibility.Visible;

            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Authorization();
        }


        private void btnFrameTeam_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageTeam());
        }

        private void btnFrameParticipant_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageParticipant());
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

        private void btnUscPersonalAccount_Click(object sender, RoutedEventArgs e)
        {
            Dialog.Show(new uscDialogAccountAdd(_idAccount, false));
        }
    }
}
